using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Iris.Messages;
using Iris.Utils;

namespace Iris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dict = new Dictionary<int, string>();
            var a = dict.Count == 0 ? 0 : dict.Keys.Max() + 1;
            dict.Add(a, "test");
            a = dict.Count == 0 ? 0 : dict.Keys.Max() + 1;
            var b = dict.Keys.Max();
        }
    }
}
