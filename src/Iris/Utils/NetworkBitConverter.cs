using System;
using System.Net;

namespace Iris.Utils
{
    public static class NetworkBitConverter
    {
        public static short ToInt16(byte[] value, int startIndex) => IPAddress.NetworkToHostOrder(BitConverter.ToInt16(value, startIndex));
        public static int ToInt32(byte[] value, int startIndex) => IPAddress.NetworkToHostOrder(BitConverter.ToInt32(value, startIndex));
        public static long ToInt64(byte[] value, int startIndex) => IPAddress.NetworkToHostOrder(BitConverter.ToInt64(value, startIndex));

        public static byte[] GetBytes(short value) => BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
        public static byte[] GetBytes(int value) => BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
        public static byte[] GetBytes(long value) => BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
    }
}
