using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iris.Messages;
using Iris.Utils;

namespace Iris.Controllers
{
    public class PingManager
    {
        public ClientControl Client { get; set; }

        public Dictionary<int, long> PingTable = new Dictionary<int, long>();
        public Dictionary<int, int> DelayTable = new Dictionary<int, int>(); // RTT

        public PingManager(ClientControl client)
        {
            Client = client;
        }

        public async Task Run()
        {
            var timer = new Timer
            {
                Enabled = true,
                Interval = 1000 * 1000 * 10
            };
            timer.TimerTick += () => Task.Run(Ping);
            await timer.Run();
        }

        public async Task Ping()
        {
            var message = new PingMessage
            {
                ClientId = Client.ClientId,
                ConnectionId = Client.ConnectionId,
                DownloadRate = TransmissionConfig.DownloadRate,
                UploadRate = TransmissionConfig.UploadRate,
            };

            PingTable.Add(message.PingId, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

            await Client.Sender.Send(message);
        }

        public async Task Respond(int pingId)
        {
            var message = new PingMessage2
            {
                ClientId = Client.ClientId,
                ConnectionId = Client.ConnectionId,
                PingId = pingId,
            };

            await Client.Sender.Send(message);
        }

        public void OnPing(PingMessage message)
        {
            Client.DownloadRate = message.DownloadRate;
            Client.UploadRate = message.UploadRate;

            Task.Run(async() =>
            {
                await Respond(message.PingId);
            });
        }

        public void OnRespond(PingMessage2 message)
        {
            var delay = (int)(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - PingTable[message.PingId]);
            DelayTable.Add(message.PingId, delay);

            if (Client.ClientId == 0)
            {
                Console.WriteLine($"Connection created with clientId={message.ClientId}");
                Client.ClientId = message.ClientId;
            }
            Console.WriteLine($"RTT {delay}ms");
        }
    }
}
