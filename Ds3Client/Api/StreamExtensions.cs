using System.IO;

namespace Ds3Client.Api
{
    static class StreamExtensions
    {
        /// <summary>
        /// Since .NET 3.5 doesn't have this extension we'll define it.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void CopyTo(this Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024]; // Fairly arbitrary size
            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                output.Write(buffer, 0, bytesRead);
        }
    }
}
