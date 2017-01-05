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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Runtime
{
    /// <summary>
    /// Percent encodes and decodes metadata keys and values. The encoding comforms to the HTTP header
    /// key requirements. Header keys contain only printable US-ASCII characters that are non-separators.
    /// </summary>
    static class MetadataUtil
    {
        /// <summary>
        /// List of printable US-ASCII characters that do not need percent encoding.
        /// Includes separators equals "=" and comma ",": not encoded for use in creating Range header
        /// Excludes separators: <![CDATA["(" | ")" | "<" | ">" | "@" | ";" | ":" | "\" | <"> | "/" | "[" | "]" | "?" | "{" | "}"]]>
        /// Excludes Percent "%": not considered safe because it is used in percent encoding
        /// Excludes Plus "+": not considered safe because its interpreted as space during decoding
        /// </summary>
        private static readonly char[] AllowedChars = { '!', '#', '$', '&', '\'', '*', '-', '.', '~', '^', '_', '`', '|', ',', '=' };

        /// <summary>
        /// Percent encodes non-alpha-numeric characters within the specified string
        /// excluding the symbols listed in <see cref="AllowedChars" /> using UTF-8
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EscapeString(string str)
        {
            if (str == null)
            {
                return null;
            }
            return Runtime.CustomPercentEscaper.PercentEncode(str, AllowedChars);
        }

        /// <summary>
        /// Decodes a percent-encoded string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnescapeString(string str)
        {
            if (str == null)
            {
                return null;
            }
            return Uri.UnescapeDataString(str);
        }

        /// <summary>
        /// Percent encodes non-alpha-numeric characters within the specified dictionary
        /// excluding the symbols listed in <see cref="AllowedChars" /> using UTF-8
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public static Dictionary<string, string> EscapeMetadata(Dictionary<string, string> metadata)
        {
            return metadata.ToDictionary(
                data => EscapeString(data.Key),
                data => EscapeString(data.Value));
        }

        /// <summary>
        /// Decodes all keys and values within a dictionary that are percent-encoded
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public static Dictionary<string, string> UnescapeMetadata(Dictionary<string, string> metadata)
        {
            return metadata.ToDictionary(
                data => UnescapeString(data.Key),
                data => UnescapeString(data.Value));
        }
    }
}
