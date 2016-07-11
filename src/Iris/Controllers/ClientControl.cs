using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Iris.Utils;

namespace Iris.Controllers
{
    public class ClientControl
    {
        // Base property
        private int _clientId;
        public int ClientId
        {
            get { return _clientId; }
            set
            {
                if (Gateway.LocalType == LocalTypes.Server && value == 0)
                {
                    _clientId = BinaryUtils.NewHashCode();
                }
                else
                {
                    _clientId = value;
                }
            }
        }

        public int ConnectionId => Gateway.Conn.ConnectionId;
        public IPEndPoint DstPoint { get; set; }

        public bool IsAlive { get; set; } = false;

        // Parent controllers
        public Gateway Gateway { get; set; }

        // Child controllers
        public UdpConnection Conn { get; set; }
        public Receiver Receiver { get; }
        public Sender Sender { get; }

        // Network controllers
        public PingManager PingManager { get; }

        // Network paramters
        public int DownloadRate { get; set; }
        public int UploadRate { get; set; }

        public ClientControl()
        {
            Receiver = new Receiver(this);
            Sender = new Sender(this);

            PingManager = new PingManager(this);
        }

        public async Task StartClient()
        {
            await PingManager.Run();
        }

        public async Task StartServer()
        {
            await PingManager.Run();
        }
    }
}
