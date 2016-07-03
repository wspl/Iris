namespace Iris.Messages
{
    public class CloseConnMessage : MessageBase<CloseConnMessage>
    {
        public CloseConnMessage() : base(BaseHeaderLength) { }
        public CloseConnMessage(byte[] dgram) : base(dgram) { }
    }
}
