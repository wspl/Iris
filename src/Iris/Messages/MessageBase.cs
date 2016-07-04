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

        //Sorter
        public static MessageBase FromDgram(byte[] dgram)
        {
            return new MessageBase(dgram);
        }

        public delegate void IsAckListMessageCallback(AckListMessage ackListMessage);
        public MessageBase IsAckListMessage(IsAckListMessageCallback callback)
        {

            if (Type == MessageType.AckListMessage)
            {
                callback(new AckListMessage(_dgram));
            }

            return this;
        }

        public delegate void IsDataMessageCallback(DataMessage dataMessage);
        public MessageBase IsDataMessage(IsDataMessageCallback callback)
        {
            if (Type == MessageType.DataMessage)
            {
                callback(new DataMessage(_dgram));
            }

            return this;
        }

        public delegate void IsPingMessageCallback(PingMessage pingMessage);
        public MessageBase IsPingMessage(IsPingMessageCallback callback)
        {
            if (Type == MessageType.PingMessage)
            {
                callback(new PingMessage(_dgram));
            }

            return this;
        }

        public delegate void IsPingMessage2Callback(PingMessage2 pingMessage2);
        public MessageBase IsPingMessage2(IsPingMessage2Callback callback)
        {
            if (Type == MessageType.PingMessage2)
            {
                callback(new PingMessage2(_dgram));
            }

            return this;
        }

        public delegate void IsCloseConnMessageCallback(CloseConnMessage closeConnMessage);
        public MessageBase IsCloseConnMessage(IsCloseConnMessageCallback callback)
        {
            if (Type == MessageType.CloseConnMessage)
            {
                callback(new CloseConnMessage(_dgram));
            }

            return this;
        }

        public delegate void IsCloseStreamMessageCallback(CloseStreamMessage closeStreamMessage);
        public MessageBase IsCloseStreamMessage(IsCloseStreamMessageCallback callback)
        {
            if (Type == MessageType.CloseStreamMessage)
            {
                callback(new CloseStreamMessage(_dgram));
            }

            return this;
        }
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

        public int ClientId
        {
            get { return NetworkBitConverter.ToInt32(_dgram, 8); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, 8); }
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
