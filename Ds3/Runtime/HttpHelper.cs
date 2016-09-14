/*
 * ******************************************************************************
 *   Copyright 2014-2016 Spectra Logic Corporation. All Rights Reserved.
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
using System.Text;

namespace Ds3.Runtime
{
    internal static class HttpHelper
    {
        private static readonly char[] _hexChars = new char[] {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        };

        /// <summary>
        /// Implements percent encoding of a URI path as specified by RFC 3986 Section 2.1
        /// http://tools.ietf.org/html/rfc3986#section-2.1
        /// 
        /// This method percent encodes the UTF-8 representation of all characters except those
        /// specified as "Unreserved" by the RFC or a forward slash (/).
        /// 
        /// We've implemented this method because all of the available built-in .NET framework
        /// methods are incomplete in one way or another.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PercentEncodePath(string path)
        {
            return PercentEncodePath(path, '/', '+');
        }

        /// <summary>
        /// Implements percent encoding of a URI path as specified by RFC 3986 Section 2.1
        /// http://tools.ietf.org/html/rfc3986#section-2.1
        /// 
        /// This method percent encodes the UTF-8 representation of all characters except those
        /// specified as "Unreserved" by the RFC or specified in the "allowedChars" parame1ter.
        /// 
        /// We've implemented this method because all of the available built-in .NET framework
        /// methods are incomplete in one way or another.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="allowedChars"></param>
        /// <returns></returns>
        public static string PercentEncodePath(string path, params char[] allowedChars)
        {
            var sb = new StringBuilder();
            var charBuffer = new char[1];
            var byteBuffer = new byte[4];
            var allowedCharSet = new HashSet<char>(allowedChars);
            foreach (var ch in path)
            {
                if (IsUnreserved(ch) || allowedCharSet.Contains(ch))
                {
                    sb.Append(ch);
                }
                else
                {
                    charBuffer[0] = ch;
                    var count = Encoding.UTF8.GetBytes(charBuffer, 0, 1, byteBuffer, 0);
                    for (var i = 0; i < count; i++)
                    {
                        sb.Append('%');
                        sb.Append(_hexChars[byteBuffer[i] >> 4]);
                        sb.Append(_hexChars[byteBuffer[i] & 0x0f]);
                    }
                }
            }
            return sb.ToString();
        }

        private static bool IsUnreserved(char ch)
        {
            return
                ('a' <= ch && ch <= 'z')
                || ('A' <= ch && ch <= 'Z')
                || ('0' <= ch && ch <= '9')
                || (ch == '-')
                || (ch == '.')
                || (ch == '_')
                || (ch == '~')
                || (ch == '(')
                || (ch == ')');
        }
    }
}
