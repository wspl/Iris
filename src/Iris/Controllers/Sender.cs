using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iris.Messages;

namespace Iris.Controllers
{
    public class Sender
    {
        private ClientControl Client { get; }

        public Sender(ClientControl client)
        {
            Client = client;
        }

        public async Task<int> Send<T>(MessageBase<T> message) where T : MessageBase<T>
        {
            return await Client.Conn.SendMessage(message, Client.DstPoint);
        }
    }
}
