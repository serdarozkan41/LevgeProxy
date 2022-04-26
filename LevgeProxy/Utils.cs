using System;
using System.Linq;

namespace LevgeProxy
{
    public class Utils
    {
        static public string ToReadableByteArray(byte[] bytes)
        {
            return string.Join(", ", bytes);
        }

        public static byte[] ParseData(string who,byte[] data)
        {
            Console.WriteLine("{0} {1}", who, BitConverter.ToString(data.Take(100).ToArray()).Replace("-", ""));
            return data;
        }
    }
}
