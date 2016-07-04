using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Iris.Controllers
{
    public class ClientControl
    {
        // Base property
        public int ClientId { get; set; }
        public IPEndPoint DstPoint { get; set; }

        // Sub controllers
        public UdpConnection Conn { get; set; }
        public Receiver Receiver { get; private set; }
        public Sender Sender { get; private set; }

        // Network controllers
        private PingControl Pinger { get; }

        public ClientControl()
        {
            Receiver = new Receiver(this);
            Sender = new Sender(this);

            Pinger = new PingControl(this);
        }
    }
}
