using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iris.Messages;

namespace Iris.Controllers
{
    public enum LocalTypes
    {
        Server, Client
    }

    public class Gateway
    {
        public LocalTypes LocalType { get; set; }
        public UdpConnection Conn { get; }

        private readonly Dictionary<int, ClientControl> _clientTable = new Dictionary<int, ClientControl>();

        private int _localClientId { get; set; }

        /// <summary>
        /// Create gateway as client
        /// </summary>
        /// <param name="remoteServer"></param>
        public Gateway(IPEndPoint remoteServer)
        {
            LocalType = LocalTypes.Client;

            Conn = new UdpConnection();
            Conn.ReceivedMessage += OnReceiveBypass;

            var client = new ClientControl
            {
                Gateway = this,
                ClientId = new Random().Next(),
                Conn = Conn,
                DstPoint = remoteServer
            };

            _localClientId = client.ClientId;
            _clientTable.Add(client.ClientId, client);
        }

        /// <summary>
        /// Create gateway as server
        /// </summary>
        /// <param name="listenPort"></param>
        public Gateway(int listenPort)
        {
            LocalType = LocalTypes.Server;

            Conn = new UdpConnection(listenPort);
            Conn.ReceivedMessage += OnReceiveBypass;
        }

        public async Task Connect()
        {
            if (LocalType == LocalTypes.Client)
            {
                await Task.WhenAll(_clientTable[_localClientId].StartClient(), Conn.HandleReceiveMessage());
            }
        }

        public async Task Listen()
        {
            if (LocalType == LocalTypes.Server)
            {
                await Conn.HandleReceiveMessage();
            }
        }

        /// <summary>
        /// Bypassing the message receive from remote. (Bypassing of sending: Sender)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void OnReceiveBypass(MessageBase message, UdpReceiveResult result)
        {
            if (!_clientTable.ContainsKey(message.ClientId))
            {
                var client = new ClientControl
                {
                    Gateway = this,
                    ClientId = message.ClientId,
                    Conn = Conn,
                    DstPoint = result.RemoteEndPoint
                };
                _clientTable.Add(message.ClientId, client);
            }
            _clientTable[message.ClientId].Receiver.OnReceive(message);
        }
    }
}
