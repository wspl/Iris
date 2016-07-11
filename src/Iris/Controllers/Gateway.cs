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

        private ClientControl LocalClient { get; }

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
                ClientId = 0,
                Conn = Conn,
                DstPoint = remoteServer
            };

            LocalClient = client;
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
                await Task.WhenAll(LocalClient.StartClient(), Conn.HandleReceiveMessage());
            }
        }

        public async Task Listen()
        {
            if (LocalType == LocalTypes.Server)
            {
                await Task.WhenAll(LocalClient.StartServer(), Conn.HandleReceiveMessage());
            }
        }

        /// <summary>
        /// Bypassing the message receive from remote. (Bypassing of sending: Sender)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void OnReceiveBypass(MessageBase message, UdpReceiveResult result)
        {
            if (LocalType == LocalTypes.Server)
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
                    // If message.ClientId is zero, clientControl will generated a clientId automantically.
                    _clientTable.Add(message.ClientId, client);
                }
                _clientTable[message.ClientId].Receiver.OnReceive(message);
            }
            else if (LocalType == LocalTypes.Client)
            {
                // Connection created when first receive PingMessage2
                if (LocalClient.ClientId == 0 && message.Type == MessageType.PingMessage2)
                {
                    LocalClient.ClientId = message.ClientId;
                }
                LocalClient.Receiver.OnReceive(message);
            }
        }
    }
}