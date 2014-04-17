using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Ds3.Runtime
{
    internal static class XmlExtensions
    {
        public static XDocument ReadDocument(Stream content)
        {
            try
            {
                return XDocument.Load(content);
            }
            catch (XmlException e)
            {
                throw new Ds3BadResponseException(e);
            }
        }

        public static Stream WriteToMemoryStream(this XDocument doc)
        {
            var stream = new MemoryStream();
            doc.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public static XElement SetAttributeValueFluent(this XElement self, string attributeName, string value)
        {
            self.SetAttributeValue(attributeName, value);
            return self;
        }

        public static T AddFluent<T>(this T self, XElement child) where T : XContainer
        {
            self.Add(child);
            return self;
        }

        public static T AddAllFluent<T>(this T self, IEnumerable<XElement> children) where T : XContainer
        {
            foreach (var child in children)
            {
                self.Add(child);
            }
            return self;
        }

        public static XAttribute AttributeOrThrow(this XElement self, string attributeName)
        {
            var element = self.Attribute(attributeName);
            if (element == null)
            {
                throw new Ds3BadResponseException(attributeName);
            }
            return element;
        }

        public static XElement ElementOrThrow(this XContainer self, string elementName)
        {
            var element = self.Element(elementName);
            if (element == null)
            {
                throw new Ds3BadResponseException(elementName);
            }
            return element;
        }

        public static string TextOf(this XContainer self, string elementName)
        {
            return self.ElementOrThrow(elementName).Value;
        }

        public static string TextOfOrNull(this XContainer self, string elementName)
        {
            var element = self.Element(elementName);
            return element == null ? null : element.Value;
        }
    }
}
