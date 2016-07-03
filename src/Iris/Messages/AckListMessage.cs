using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iris.Utils;

namespace Iris.Messages
{
    public class AckListMessage : MessageBase<AckListMessage>
    {
        public int LastRead
        {
            get { return NetworkBitConverter.ToInt32(_dgram, BaseHeaderLength); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, BaseHeaderLength);  }
        }

        public AckListMessage SetLastRead(int lastRead)
        {
            LastRead = lastRead;
            return this;
        }

        public short AckSize
        {
            get { return NetworkBitConverter.ToInt16(_dgram, BaseHeaderLength + 4); }
            private set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, BaseHeaderLength + 4); }
        }

        private const int HeaderLength = BaseHeaderLength + 6;

        public ReadOnlyCollection<int> AckList
        {
            get
            {
                var ackList = new List<int>();
                for (var i = 0; i < AckSize; i += 1)
                {
                    ackList.Add(NetworkBitConverter.ToInt32(_dgram, HeaderLength + 4*i));
                }
                return ackList.AsReadOnly();
            }
            set
            {
                AckSize = (short)value.Count;
                for (var i = 0; i < value.Count; i += 1)
                {
                    var sequence = value[i];
                    NetworkBitConverter.GetBytes(sequence).CopyTo(_dgram, HeaderLength + 4 * i);
                }
                Size = WithoutFooterLength + 24;
            }
        }

        public AckListMessage SetAckList(ReadOnlyCollection<int> ackList)
        {
            AckList = ackList;
            return this;
        }

        private int WithoutFooterLength => HeaderLength + AckSize*4 + 8;

        public int R1
        {
            get { return NetworkBitConverter.ToInt32(_dgram, WithoutFooterLength); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, WithoutFooterLength); }
        }

        public int S1
        {
            get { return NetworkBitConverter.ToInt32(_dgram, WithoutFooterLength + 4); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, WithoutFooterLength + 4); }
        }

        public int R2
        {
            get { return NetworkBitConverter.ToInt32(_dgram, WithoutFooterLength + 8); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, WithoutFooterLength + 8); }
        }

        public int S2
        {
            get { return NetworkBitConverter.ToInt32(_dgram, WithoutFooterLength + 12); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, WithoutFooterLength + 12); }
        }

        public int R3
        {
            get { return NetworkBitConverter.ToInt32(_dgram, WithoutFooterLength + 16); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, WithoutFooterLength + 16); }
        }

        public int S3
        {
            get { return NetworkBitConverter.ToInt32(_dgram, WithoutFooterLength + 20); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, WithoutFooterLength + 20); }
        }



        public AckListMessage SetSendRecord(Dictionary<int, SendRecord> sendRecordTable, int timeId)
        {
            R1 = timeId - 2;
            S1 = sendRecordTable?[R1].SentSize ?? 0;

            R2 = timeId - 1;
            S2 = sendRecordTable?[R2].SentSize ?? 0;

            R3 = timeId;
            S3 = sendRecordTable?[R3].SentSize ?? 0;

            return this;
        }

        public AckListMessage() { }
        public AckListMessage(byte[] dgram) : base(dgram) { }
    }
}
