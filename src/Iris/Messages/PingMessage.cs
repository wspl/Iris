using Iris.Utils;

namespace Iris.Messages
{
    public class PingMessage : MessageBase<PingMessage>
    {
        public int PingId
        {
            get { return NetworkBitConverter.ToInt32(_dgram, BaseHeaderLength); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, BaseHeaderLength); }
        }

        public int DownloadRate
        {
            get { return NetworkBitConverter.ToInt32(_dgram, BaseHeaderLength + 4); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, BaseHeaderLength + 4); }
        }

        public int UploadRate
        {
            get { return NetworkBitConverter.ToInt32(_dgram, BaseHeaderLength + 8); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, BaseHeaderLength + 8); }
        }

        private const int HeaderLength = BaseHeaderLength + 12;

        public PingMessage() : base(HeaderLength) { }
        public PingMessage(byte[] dgram) : base(dgram) { }
    }
}
