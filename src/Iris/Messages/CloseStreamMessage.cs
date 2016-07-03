using Iris.Utils;

namespace Iris.Messages
{
    public class CloseStreamMessage : MessageBase<CloseStreamMessage>
    {
        public int CloseOffset
        {
            get { return NetworkBitConverter.ToInt32(_dgram, BaseHeaderLength); }
            set { NetworkBitConverter.GetBytes(value).CopyTo(_dgram, BaseHeaderLength); }
        }

        private const int HeaderLength = BaseHeaderLength + 4;

        public CloseStreamMessage() : base(HeaderLength) { }
        public CloseStreamMessage(byte[] dgram) : base(dgram) { }
    }
}
