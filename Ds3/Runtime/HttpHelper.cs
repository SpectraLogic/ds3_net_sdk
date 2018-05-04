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
using System.Linq;

namespace Ds3.Runtime
{
    internal static class HttpHelper
    {
        private static readonly IEnumerable<char> Alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
        private static readonly IEnumerable<char> Digit = "0123456789".ToArray();
        private static readonly IEnumerable<char> Unreserved = Alpha.Concat(Digit).Concat("-._~".ToArray());
        private static readonly IEnumerable<char> SubDelims = "!$&'()*+,;=".ToArray();

        private static readonly IEnumerable<char> Pchar = Unreserved.Concat(SubDelims).Concat(":@".ToArray());


        /// <summary>
        /// Specified as "Query" by the RFC, forward slash (/) and question mark (?) without semicolon (;) and plus (+)
        /// </summary>
        private static readonly char[] AllowedCharsQuery = Pchar.Concat("/?".ToArray()).Where(ch => ch != ';' && ch != '+').ToArray();

        /// <summary>
        /// Specified as "Path" by the RFC, forward slash (/) without semicolon (;)
        /// </summary>
        private static readonly char[] AllowedCharsPath = Pchar.Concat("/".ToArray()).Where(ch => ch != ';').ToArray();

        /// <summary>
        /// Implements percent encoding of a URI path as specified by RFC 3986
        /// http://tools.ietf.org/html/rfc3986
        /// 
        /// This method percent encodes the UTF-8 representation of all characters except those
        /// specified as "Path" by the RFC, forward slash (/) and without semicolon (;).
        /// 
        /// We've implemented this method because all of the available built-in .NET framework
        /// methods are incomplete in one way or another.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PercentEncodePath(string path)
        {
            return CustomPercentEscaper.PercentEncode(path, AllowedCharsPath);
        }

        /// <summary>
        /// Implements percent encoding of a URI path as specified by RFC 3986
        /// http://tools.ietf.org/html/rfc3986
        /// 
        /// This method percent encodes the UTF-8 representation of all characters except those
        /// specified as "Param" by the RFC, forward slash (/) and question mark (?) without semicolon (;) and plus (+).
        /// 
        /// We've implemented this method because all of the available built-in .NET framework
        /// methods are incomplete in one way or another.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PercentEncodeQuery(string path)
        {
            return CustomPercentEscaper.PercentEncode(path, AllowedCharsQuery);
        }
    }
}
