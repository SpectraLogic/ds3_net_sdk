/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */

using System.Collections.Generic;
using System.IO;
using System.Text;
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
                return XDocument.Load(new XmlNoNamespaceReader(content));
            }
            catch (XmlException e)
            {
                throw new Ds3BadResponseException(e);
            }
        }

        public static Stream WriteToMemoryStream(this XDocument doc)
        {
            var stream = new MemoryStream();
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Encoding = new UTF8Encoding(false)
            };
            using (var xmlWriter = XmlWriter.Create(stream, settings))
            {
                doc.Save(xmlWriter);
            }
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public static XElement SetAttributeValueFluent(this XElement self, string attributeName, string value)
        {
            self.SetAttributeValue(attributeName, value);
            return self;
        }

        public static XElement SetValueFluent(this XElement self, string value)
        {
            self.SetValue(value);
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
                throw new Ds3BadResponseException(Ds3BadResponseException.ExpectedItemType.XmlElement, attributeName);
            }
            return element;
        }

        public static string AttributeText(this XElement self, string attributeName)
        {
            return self.AttributeOrThrow(attributeName).Value;
        }

        public static string AttributeTextOrNull(this XElement self, string attributeName)
        {
            var attribute = self.Attribute(attributeName);
            return attribute == null ? null : attribute.Value;
        }

        public static XElement ElementOrThrow(this XContainer self, string elementName)
        {
            var element = self.Element(elementName);
            if (element == null)
            {
                throw new Ds3BadResponseException(Ds3BadResponseException.ExpectedItemType.XmlElement, elementName);
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

        private class XmlNoNamespaceReader : XmlTextReader
        {
            public XmlNoNamespaceReader(Stream stream)
                : base(stream)
            {
            }

            public override string NamespaceURI
            {
                get { return ""; }
            }
        }
    }
}
