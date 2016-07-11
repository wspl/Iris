using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iris.Messages;

namespace Iris.Controllers
{
    public class UdpConnection
    {
        public const bool SimulateBadNetwork = true;

        public int ConnectionId => GetHashCode();

        private UdpClient Socket { get; }

        public UdpConnection(int listenPort)
        {
            Socket = new UdpClient(listenPort);
        }

        public UdpConnection()
        {
            Socket = new UdpClient(0, AddressFamily.InterNetwork);
        }

        public async Task<int> SendMessage<T>(MessageBase<T> message, IPEndPoint target) where T : MessageBase<T>
        {
            if (SimulateBadNetwork)
            {
                var random = new Random();
                if (random.Next(0, 100) > 95)
                {
                    return 0;
                }
                await Task.Delay(random.Next(100, 200));
            }
            return await Socket.SendAsync(message.Dgram, message.Size, target);
        }

        public async Task HandleReceiveMessage()
        {
            while (true)
            {
                var result = await Socket.ReceiveAsync();
                OnReceiveMessage(new MessageBase(result.Buffer), result);
            }
        }

        public delegate void ReceiveMessageEventHandler(MessageBase message, UdpReceiveResult result);

        public event ReceiveMessageEventHandler ReceivedMessage;

        protected virtual void OnReceiveMessage(MessageBase message, UdpReceiveResult result)
        {
            ReceivedMessage?.Invoke(message, result);
        }
    }
}
