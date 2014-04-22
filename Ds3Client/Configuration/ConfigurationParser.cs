/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
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

using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Ds3Client.Configuration
{
    class ConfigurationParser
    {
        public static Configuration Parse(Stream stream)
        {
            XDocument doc;
            using (var reader = XmlReader.Create(stream))
            {
                doc = XDocument.Load(reader);
            }
            Func<string, string> value = elementName =>
            {
                var element = doc.Root.Element(elementName);
                return element == null ? null : element.Value;
            };
            return new Configuration
            {
                Name = value("Name"),
                Endpoint = MakeUriOrNull(value("Endpoint")),
                Proxy = MakeUriOrNull(value("Proxy")),
                AccessKey = value("AccessKey"),
                SecretKey = value("SecretKey"),
                IsSelected = false
            };
        }

        private static Uri MakeUriOrNull(string value)
        {
            try
            {
                return value == null ? null : new Uri(value);
            }
            catch (UriFormatException)
            {
                return null;
            }
        }

        public static void Unparse(Configuration configuration, Stream stream)
        {
            var root = new XElement("Configuration");
            root.Add(new XElement("Endpoint", configuration.Endpoint.ToString()));
            if (configuration.Proxy != null)
            {
                root.Add(new XElement("Proxy", configuration.Proxy.ToString()));
            }
            root.Add(new XElement("AccessKey", configuration.AccessKey));
            root.Add(new XElement("SecretKey", configuration.SecretKey));

            var doc = new XDocument();
            doc.Add(root);

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true }))
            {
                doc.WriteTo(writer);
            }
        }

        public static void ValidateConfiguration(Configuration config)
        {
            if (config.Name == null)
            {
                throw new ConfigurationException(Resources.MissingNameException);
            }
            if (!IsValidName(config.Name))
            {
                throw new ConfigurationException(Resources.InvalidNameException, config.Name);
            }

            if (config.Endpoint == null)
            {
                throw new ConfigurationException(Resources.MissingOrInvalidEndpointException);
            }
            if (!IsValidEndpoint(config.Endpoint))
            {
                throw new ConfigurationException(Resources.InvalidEndpointException, config.Endpoint);
            }

            if (!IsValidBase64String(config.AccessKey))
            {
                throw new ConfigurationException(Resources.MissingAccessKeyException);
            }

            if (!IsValidBase64String(config.SecretKey))
            {
                throw new ConfigurationException(Resources.MissingSecretKeyException);
            }
        }

        private static bool IsValidBase64String(string value)
        {
            if (value == null)
            {
                return false;
            }
            try
            {
                Convert.FromBase64String(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool IsValidName(string filename)
        {
            return filename.Intersect(Path.GetInvalidFileNameChars()).Count() == 0;
        }

        private static bool IsValidEndpoint(Uri endpoint)
        {
            return new[] { "http", "https" }.Contains(endpoint.Scheme);
        }
    }
}
