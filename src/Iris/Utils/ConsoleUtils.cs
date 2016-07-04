using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Utils
{
    public static class ConsoleUtils
    {
        private const int ReadlineBufferSize = 10000;

        public static string ReadLine()
        {
            var inputStream = Console.OpenStandardInput();
            var bytes = new byte[ReadlineBufferSize];
            var outputLength = inputStream.Read(bytes, 0, ReadlineBufferSize);
            var chars = Encoding.UTF7.GetChars(bytes, 0, outputLength);
            return new string(chars).TrimEnd();
        }
    }
}
