using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iris
{
    public static class TransmissionConfig
    {
        public const short ProtocolVersion = 1;
        public const int MaxPacketSize = 1500;

        public const int UploadRate = 1000000;
        public const int DownloadRate = 100000;
    }
}
