using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iris.Messages;

namespace Iris.Controllers
{
    public class Receiver
    {
        private ClientControl Client { get; }

        public Receiver(ClientControl client)
        {
            Client = client;
        }

        public void OnReceive(MessageBase message)
        {
            message
                .IsPingMessage(Client.PingManager.OnPing)
                .IsPingMessage2(Client.PingManager.OnRespond);
        }
    }
}
