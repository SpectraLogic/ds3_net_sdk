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


namespace Ds3.Runtime
{
    internal static class HttpHelper
    {

        /// <summary>
        /// Specified as "Unreserved" by the RFC
        /// </summary>
        private static readonly char[] UnreservedCharsParam = { '-', '.', '_', '~', '(', ')' };

        /// <summary>
        /// Specified as "Unreserved" by the RFC, plus (+), and forward slash (/)
        /// </summary>
        private static readonly char[] UnreservedCharsPath = { '-', '.', '_', '~', '(', ')' , '+', '/' };

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
            return CustomPercentEscaper.PercentEncode(path, UnreservedCharsPath);
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
        /// <returns></returns>
        public static string PercentEncodeParam(string path)
        {
            return CustomPercentEscaper.PercentEncode(path, UnreservedCharsParam);
        }
    }
}
