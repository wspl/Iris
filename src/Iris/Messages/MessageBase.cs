using System;
using System.Net;
using Iris.Utils;

namespace Iris.Messages
{
    public class MessageBase : MessageBase<MessageBase>
    {
        public MessageBase(int size) : base(size) {}
        public MessageBase() {}
        public MessageBase(byte[] dgram) : base(dgram) {}
    }

    public class MessageBase<T> where T: MessageBase<T>
    {
        public const int TypeCode = 0;

        protected readonly byte[] _dgram = new byte[TransmissionConfig.MaxPacketSize];

        public byte[] Dgram
        {
            get
            {
                var dgram = new byte[Size];
                Array.Copy(_dgram, 0, dgram, 0, Size);
                return dgram;
            }
        }

        public int Size { get; set; }

        public short Version
        {
            get { return NetworkBitConverter.ToInt16(_dgram, 0); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, 0); }
        }

        public MessageType Type
        {
            get { return (MessageType)NetworkBitConverter.ToInt16(_dgram, 2); }
            private set { NetworkBitConverter.GetBytes((short)value).CopyTo(_dgram, 2); }
        }

        public int ConnectionId
        {
            get { return NetworkBitConverter.ToInt32(_dgram, 4); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, 4); }
        }

        public T SetConnectionId(int connectionId)
        {
            ConnectionId = connectionId;
            return (T)this;
        }

        public int ClientId
        {
            get { return NetworkBitConverter.ToInt32(_dgram, 8); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, 8); }
        }

        public T SetClientId(int clientId)
        {
            ClientId = clientId;
            return (T)this;
        }

        protected const int BaseHeaderLength = 12;

        public IPEndPoint DstHost { get; set; }

        protected MessageBase(int size)
        {
            Size = size;

            Version = TransmissionConfig.ProtocolVersion;
            Type = (MessageType)Enum.Parse(typeof (MessageType), typeof (T).Name);
        }

        protected MessageBase() : this(TransmissionConfig.MaxPacketSize) {}

        protected MessageBase(byte[] dgram)
        {
            _dgram = dgram;
        }
    }

    public enum MessageType
    {
        AckListMessage = 60,

        CloseStreamMessage = 75,
        CloseConnMessage = 76,

        DataMessage = 80,

        PingMessage = 301,
        PingMessage2 = 302,
    }
}
