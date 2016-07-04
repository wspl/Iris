using System;
using Iris.Utils;

namespace Iris.Messages
{
    public class DataMessage : MessageBase<DataMessage>
    {
        public int Sequence
        {
            get { return NetworkBitConverter.ToInt32(_dgram, BaseHeaderLength); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, BaseHeaderLength); }
        }

        public short DataLength
        {
            get { return NetworkBitConverter.ToInt16(_dgram, BaseHeaderLength + 4); }
            private set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, BaseHeaderLength + 4); }
        }

        public int TimeId
        {
            get { return NetworkBitConverter.ToInt32(_dgram, BaseHeaderLength + 6); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, BaseHeaderLength + 6); }
        }

        private const int HeaderLength = BaseHeaderLength + 10;

        public byte[] Data
        {
            get
            {
                var data = new byte[DataLength];
                Array.Copy(_dgram, HeaderLength, data, 0, DataLength);
                return data;
            }
            set
            {
                DataLength = (short)value.Length;
                Array.Copy(value, 0, _dgram, HeaderLength, DataLength);
                Size = HeaderLength + DataLength;
            }
        }

        public DataMessage() { }
        public DataMessage(byte[] dgram) : base(dgram) { }
    }
}
