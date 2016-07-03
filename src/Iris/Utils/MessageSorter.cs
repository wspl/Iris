using Iris.Messages;

namespace Iris.Utils
{
    public class MessageSorter
    {
        private readonly byte[] _dgram;
        private readonly MessageBase _baseMessage;

        public static MessageSorter FromDgram(byte[] dgram)
        {
            return new MessageSorter(dgram);
        }

        public MessageSorter(byte[] dgram)
        {
            _dgram = dgram;
            _baseMessage = new MessageBase(dgram);
        }

        public delegate void IsAckListMessageCallback(AckListMessage ackListMessage);
        public MessageSorter IsAckListMessage(IsAckListMessageCallback callback)
        {
            
            if (_baseMessage.Type == MessageType.AckListMessage)
            {
                callback(new AckListMessage(_dgram));
            }
            
            return this;
        }

        public delegate void IsDataMessageCallback(DataMessage dataMessage);
        public MessageSorter IsDataMessage(IsDataMessageCallback callback)
        {
            if (_baseMessage.Type == MessageType.DataMessage)
            {
                callback(new DataMessage(_dgram));
            }

            return this;
        }

        public delegate void IsPingMessageCallback(PingMessage pingMessage);
        public MessageSorter IsPingMessage(IsPingMessageCallback callback)
        {
            if (_baseMessage.Type == MessageType.PingMessage)
            {
                callback(new PingMessage(_dgram));
            }

            return this;
        }

        public delegate void IsPingMessage2Callback(PingMessage2 pingMessage2);
        public MessageSorter IsPingMessage2(IsPingMessage2Callback callback)
        {
            if (_baseMessage.Type == MessageType.PingMessage2)
            {
                callback(new PingMessage2(_dgram));
            }

            return this;
        }

        public delegate void IsCloseConnMessageCallback(CloseConnMessage closeConnMessage);
        public MessageSorter IsCloseConnMessage(IsCloseConnMessageCallback callback)
        {
            if (_baseMessage.Type == MessageType.CloseConnMessage)
            {
                callback(new CloseConnMessage(_dgram));
            }

            return this;
        }

        public delegate void IsCloseStreamMessageCallback(CloseStreamMessage closeStreamMessage);
        public MessageSorter IsCloseStreamMessage(IsCloseStreamMessageCallback callback)
        {
            if (_baseMessage.Type == MessageType.CloseStreamMessage)
            {
                callback(new CloseStreamMessage(_dgram));
            }

            return this;
        }

//        public delegate void IsMessageCallback<T>(T message);
//        public MessageSorter IsMessage<T>(IsMessageCallback<T> callback)
//        {
//            if (_baseMessage.Type == (MessageType)Enum.Parse(typeof(MessageType), typeof(T).Name))
//            {
//                callback(new T(_dgram));
//            }
//            return this;
//        }
    }
}
