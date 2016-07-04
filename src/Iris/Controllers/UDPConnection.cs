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
        private UdpClient Socket { get; }

        public UdpConnection(Gateway gateway)
        {
            Socket = new UdpClient();
        }

        public Task<int> SendMessage(MessageBase message, IPEndPoint target)
        {
            return Socket.SendAsync(message.Dgram, message.Size, target);
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
