using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iris.Controllers
{
    public class Sender
    {
        private ClientControl Client { get; }

        public Sender(ClientControl client)
        {
            Client = client;
        }
    }
}
