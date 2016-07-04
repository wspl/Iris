using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Iris.Controllers;
using Iris.Utils;

namespace Iris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.WaitAll(Task.Run(async () =>
            {
                var server = new Gateway(32190);
                await server.Listen();
            }), Task.Run(async () =>
            {
                var client = new Gateway(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 32190));
                await client.Connect();
            }));
        }
    }
}
