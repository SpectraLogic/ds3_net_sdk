using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ds3.Runtime
{
    public class Ds3BadResponseException : Ds3RequestException
    {
        internal Ds3BadResponseException(string expectedElementName)
            : base(BuildMissingElementMessage(expectedElementName))
        {
        }

        internal Ds3BadResponseException(XmlException innerException)
            : base(BuildResponseParseException(innerException), innerException)
        {
        }

        private static string BuildMissingElementMessage(string expectedElementName)
        {
            return string.Format(Resources.MissingElementException, expectedElementName);
        }

        private static string BuildResponseParseException(XmlException innerException)
        {
            return string.Format(Resources.XmlResponseErrorException, innerException.Message);
        }
    }
}
