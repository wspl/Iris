using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iris.Messages;
using Iris.Utils;

namespace Iris
{
    public class Program
    {
        public const int ReadlineBufferSize = 10000;
        private static string ReadLine()
        {
            var inputStream = Console.OpenStandardInput();
            var bytes = new byte[ReadlineBufferSize];
            var outputLength = inputStream.Read(bytes, 0, ReadlineBufferSize);
            var chars = Encoding.UTF7.GetChars(bytes, 0, outputLength);
            return new string(chars).TrimEnd();
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static void Main(string[] args)
        {
            var hex = ReadLine();
            var dgram = StringToByteArray(hex);
            MessageSorter.FromDgram(dgram)
                .IsAckListMessage(msg => {

                })
                .IsCloseConnMessage(msg => {
                    throw new Exception("error");
                });
        }
    }
}
