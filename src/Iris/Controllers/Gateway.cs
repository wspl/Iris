using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iris.Messages;

namespace Iris.Controllers
{
    public enum LocalTypes
    {
        Server,
        Client
    }

    public class Gateway
    {
        private UdpConnection Conn { get; set; }

        private readonly Dictionary<int, ClientControl> _clientTable = new Dictionary<int, ClientControl>();

        public Gateway()
        {
            Conn.ReceivedMessage += OnReceiveBypass;
        }

        public void OnReceiveBypass(MessageBase message, UdpReceiveResult result)
        {
            if (!_clientTable.ContainsKey(message.ClientId))
            {
                var clientId = _clientTable.Count == 0? 0 : _clientTable.Keys.Max() + 1;
                var client = new ClientControl()
                {
                    ClientId = clientId,
                    Conn = Conn,
                    DstPoint = result.RemoteEndPoint
                };
                _clientTable.Add(clientId, client);
            }
            //ClientTable[message.ClientId].Receiver
        }
    }
}
