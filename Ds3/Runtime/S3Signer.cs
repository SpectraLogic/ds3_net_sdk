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

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Ds3.Runtime
{
    internal class S3Signer
    {
        public static string AuthField(
            Credentials creds,
            string verb,
            string date,
            string resourcePath,
            string md5 = "",
            string contentType = "",
            IDictionary<string, string> amzHeaders = null)
        {
            var payload = BuildPayload(verb, date, resourcePath, md5, contentType, amzHeaders ?? new Dictionary<string, string>());
            return "AWS " + creds.AccessId + ":" + S3Signer.Signature(creds.Key, payload);
        }

        private static string Signature(string key, string payload)
        {
            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] hashResult = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return System.Convert.ToBase64String(hashResult).Trim();
        }

        private static string BuildPayload(string verb, string date, string resourcePath, string md5, string contentType, IDictionary<string, string> amzHeaders)
        {
            var builder = new StringBuilder();
            builder.Append(verb).Append('\n');
            builder.Append(md5).Append('\n');
            builder.Append(contentType).Append('\n');
            builder.Append(date).Append('\n');
            var canonicalizedAmzHeaders =
                from keyValuePair in amzHeaders
                let key = keyValuePair.Key.ToLowerInvariant()
                where key.StartsWith(HttpHeaders.AwsPrefix)
                orderby key
                select new { Key = key, Value = UnfoldLongHeaderContent(keyValuePair.Value) };
            foreach (var keyValuePair in canonicalizedAmzHeaders)
            {
                builder.Append(keyValuePair.Key).Append(':').Append(keyValuePair.Value).Append('\n');
            }
            builder.Append(resourcePath);
            return builder.ToString();
        }

        private static readonly Regex _unfoldRegex = new Regex("[\n\r\t ]+");
        private static string UnfoldLongHeaderContent(string headerContent)
        {
            return _unfoldRegex.Replace(headerContent, " ");
        }
    }
}
