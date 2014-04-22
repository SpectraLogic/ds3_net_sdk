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

using System.Text;
using System.Security.Cryptography;

namespace Ds3.Runtime
{
    internal class S3Signer
    {
        
        public static string AuthField(Credentials creds, string verb, string date, string resourcePath, string _md5 = "", string _contentType = "", string _amzHeaders = "")
        {
            string signature = S3Signer.Signature(creds.Key, BuildPayload(verb, date, resourcePath, _md5, _contentType, _amzHeaders));
            return "AWS " + creds.AccessId + ":" + signature;
        }

        private static string Signature(string key, string payload)
        {
            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] hashResult = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return System.Convert.ToBase64String(hashResult).Trim();
        }

        private static string BuildPayload(string verb, string date, string resourcePath, string md5 = "", string contentType = "", string amzHeaders = "")
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(verb).Append("\n");
            builder.Append(md5).Append("\n");
            builder.Append(contentType).Append("\n");
            builder.Append(date).Append("\n");
            builder.Append(amzHeaders).Append(resourcePath);
            return builder.ToString();
        }

    }
}
