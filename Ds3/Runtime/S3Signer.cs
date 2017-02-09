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
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Ds3.Runtime
{
    internal class S3Signer
    {
        /// <summary>
        /// List of resources that must be a part of the CanonicalizedResource Element.
        /// See "Constructing the CanonicalizedResource Element" -> "Launch Process" -> table row 4
        /// http://docs.aws.amazon.com/AmazonS3/latest/dev/RESTAuthentication.html
        /// </summary>
        private static readonly ISet<string> _subresourcesToCanonicalize = new HashSet<string>
        {
            "acl",
            "lifecycle",
            "location",
            "logging",
            "notification",
            "partNumber",
            "policy",
            "requestPayment",
            "torrent",
            "uploadId",
            "uploads",
            "versionId",
            "versioning",
            "versions",
            "website",
            "delete"
        };

        public static string AuthField(
            Credentials creds,
            string verb,
            string date,
            string resourcePath,
            IDictionary<string, string> queryString,
            string checksumValue = "",
            string contentType = "",
            IDictionary<string, string> amzHeaders = null)
        {
            var payload = BuildPayload(
                verb,
                date,
                resourcePath,
                queryString,
                checksumValue,
                contentType,
                amzHeaders ?? new Dictionary<string, string>()
            );
            return "AWS " + creds.AccessId + ":" + S3Signer.Signature(creds.Key, payload);
        }

        private static string Signature(string key, string payload)
        {
            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] hashResult = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return System.Convert.ToBase64String(hashResult).Trim();
        }

        private static string BuildPayload(
            string verb,
            string date,
            string resourcePath,
            IDictionary<string, string> queryString,
            string md5,
            string contentType,
            IDictionary<string, string> amzHeaders)
        {
            var builder = new StringBuilder();
            builder.Append(verb).Append('\n');
            builder.Append(md5).Append('\n');
            builder.Append(contentType).Append('\n');
            builder.Append(date).Append('\n');

            var canonicalizedAmzHeaders =
                amzHeaders.
                    Select(keyValuePair => new {Key = keyValuePair.Key.ToLowerInvariant(), keyValuePair.Value})
                    .Where(keyValuePair => keyValuePair.Key.StartsWith(HttpHeaders.AwsPrefix))
                    .OrderBy(keyValuePair => keyValuePair.Key, StringComparer.Ordinal)
                    .Select(
                        keyValuePair => new {keyValuePair.Key, Value = UnfoldLongHeaderContent(keyValuePair.Value)});

            foreach (var keyValuePair in canonicalizedAmzHeaders)
            {
                builder.Append(keyValuePair.Key).Append(':').Append(keyValuePair.Value).Append('\n');
            }
            builder.Append(HttpHelper.PercentEncodePath(resourcePath));
            var canonicalizedSubresources =
                from kvp in queryString
                where _subresourcesToCanonicalize.Contains(kvp.Key)
                orderby kvp.Key
                select kvp;
            var delimiter = '?';
            foreach (var keyValuePair in canonicalizedSubresources)
            {
                builder.Append(delimiter);
                builder.Append(keyValuePair.Key);
                if (keyValuePair.Value != null && keyValuePair.Value.Length > 0)
                {
                    builder.Append('=');
                    builder.Append(keyValuePair.Value);
                }
                delimiter = '&';
            }
            return builder.ToString();
        }

        private static readonly Regex _unfoldRegex = new Regex("[\n\r\t ]+");
        private static string UnfoldLongHeaderContent(string headerContent)
        {
            return _unfoldRegex.Replace(headerContent, " ");
        }
    }
}
