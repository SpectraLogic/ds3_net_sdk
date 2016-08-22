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

using Ds3.Models;
using Ds3.Runtime;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace Ds3.ResponseParsers
{
    internal class ResponseParseUtilities
    {
        public static IDictionary<string, string> ExtractCustomMetadata(IDictionary<string, string> headers)
        {
            return headers
                .Keys
                .Where(key => key.StartsWith(HttpHeaders.AwsMetadataPrefix))
                .ToDictionary(key => key.Substring(HttpHeaders.AwsMetadataPrefix.Length), key => headers[key]);
        }

        internal static JobStatus ParseJobStatus(string jobStatus)
        {
            switch (jobStatus)
            {
                case "COMPLETED": return JobStatus.COMPLETED;
                case "CANCELLED": return JobStatus.CANCELED;
                case "IN_PROGRESS": return JobStatus.IN_PROGRESS;
                default: return JobStatus.IN_PROGRESS;
            }
        }

        public static void HandleStatusCode(IWebResponse response, params HttpStatusCode[] expectedStatusCodes)
        {
            HttpStatusCode actualStatusCode = response.StatusCode;
            if (!expectedStatusCodes.Contains(actualStatusCode))
            {
                var responseContent = GetResponseContent(response);
                var error = ParseError(responseContent);
                throw new Ds3BadStatusCodeException(expectedStatusCodes, actualStatusCode, error, responseContent);
            }
        }

        private static string GetResponseContent(IWebResponse response)
        {
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        private static Error ParseError(string responseContent)
        {
            try
            {
                var root = XDocument.Parse(responseContent).ElementOrThrow("Error");
                Error error = new Error();
                error.Code = root.TextOf("Code");
                error.Message = root.TextOf("Message");
                error.Resource = root.TextOf("Resource");
                error.ResourceId = ModelParsers.ParseNullableLong(root.TextOfOrNull("RequestId")) ?? ModelParsers.ParseLong(root.TextOfOrNull("ResourceId"));
                return error;
            }
            catch (XmlException)
            {
                return null;
            }
            catch (Ds3BadResponseException)
            {
                return null;
            }
        }

        public static int? ParseIntHeader(string key, IDictionary<string, string> headers)
        {
            if (!headers.ContainsKey(key))
            {
                return null;
            }
            try
            {
                return int.Parse(headers[key]);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
