using Ds3.Models;
using System.IO;
using System.Text;

namespace TestDs3
{
    static class Helpers
    {
        public static string ReadContentStream(Ds3Request request)
        {
            using (var stream = request.GetContentStream())
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        internal static Stream StringToStream(string responseString)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(responseString));
        }
    }
}
