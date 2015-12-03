using System.IO;

namespace Ds3.Lang
{
    public static class StreamsUtil
    {
        public static void BufferedCopyTo(Stream src, Stream dst, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            var totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = src.Read(buffer, totalBytesRead, bufferSize - totalBytesRead)) != 0)
            {
                totalBytesRead += bytesRead;
                if (totalBytesRead != bufferSize) continue;
                dst.Write(buffer, 0, totalBytesRead);
                totalBytesRead = 0;
            }

            if (totalBytesRead > 0)
            {
                dst.Write(buffer, 0, totalBytesRead);
            }
        }
    }
}