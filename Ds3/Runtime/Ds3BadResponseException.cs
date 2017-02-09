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

using System;
using System.Xml;

namespace Ds3.Runtime
{
    public class Ds3BadResponseException : Ds3RequestException
    {
        public Ds3BadResponseException(ExpectedItemType expectedItemType, string expectedElementName)
            : base(BuildMissingItemMessage(expectedItemType, expectedElementName))
        {
        }

        public Ds3BadResponseException(XmlException innerException)
            : base(BuildResponseParseException(innerException), innerException)
        {
        }

        private static string BuildMissingItemMessage(ExpectedItemType expectedItemType, string expectedElementName)
        {
            switch (expectedItemType)
            {
                case ExpectedItemType.Header:
                    return string.Format(Resources.MissingHeaderException, expectedElementName);
                case ExpectedItemType.XmlElement:
                    return string.Format(Resources.MissingElementException, expectedElementName);
                default:
                    throw new IndexOutOfRangeException(Resources.InvalidEnumValueException);
            }
        }

        private static string BuildResponseParseException(XmlException innerException)
        {
            return string.Format(Resources.XmlResponseErrorException, innerException.Message);
        }

        public enum ExpectedItemType
        {
            Header,
            XmlElement
        }
    }
}
