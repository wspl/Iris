using Iris.Utils;

namespace Iris.Messages
{
    public class PingMessage2 : MessageBase<PingMessage>
    {
        public int PingId
        {
            get { return NetworkBitConverter.ToInt32(_dgram, BaseHeaderLength); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, BaseHeaderLength); }
        }

        private const int HeaderLength = BaseHeaderLength + 4;

        public PingMessage2() : base(HeaderLength) { }
        public PingMessage2(byte[] dgram) : base(dgram) { }
    }
}
