using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iris.Controllers;
using Iris.Messages;
using Iris.Utils;

namespace Iris
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.ReadKey();
//            var guid = Guid.NewGuid().GetHashCode();
//
//
//            var udpClient = new UdpClient(54321);
//            var clientId = 12345;
//            Task.WaitAll(Task.Run(async () =>
//            {
//                while (true)
//                {
//                    var msg = new PingMessage
//                    {
//                        ClientId = clientId,
//                        ConnectionId = 1,
//                        DownloadRate = 12345,
//                        UploadRate = 12345,
//                        PingId = 0
//                    };
//                    //await udpClient.SendAsync(msg.Dgram, msg.Size, "127.0.0.1", 5002);
//                    await Task.Delay(1000);
//                }
//            }), Task.Run(async () =>
//            {
//                while (true)
//                {
//                    var rs = await udpClient.ReceiveAsync();
//                    var msg = MessageBase.FromDgram(rs.Buffer);
//                    clientId = msg.ClientId;
//                }
//            }));
//            Task.WaitAll(Task.Run(async () =>
//            {
////                var server = new Gateway(32190);
////                await server.Listen();
//            }), Task.Run(async () =>
//            {
//                var client = new Gateway(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5002));
//                await client.Connect();
//            }));
        }
    }
}
