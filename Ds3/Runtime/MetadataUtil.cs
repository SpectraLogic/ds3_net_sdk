using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ds3.Runtime
{
    /// <summary>
    /// Percent encodes and decodes metadata keys and values. The encoding comforms to the HTTP header
    /// key requirements. Header keys contain only printable US-ASCII characters that are non-separators.
    /// </summary>
    class MetadataUtil
    {
        /// <summary>
        /// List of printable US-ASCII characters that do not need percent encoding.
        /// Includes separators equals "=" and comma ",": not encoded for use in creating Range header
        /// Excludes separators: <![CDATA["(" | ")" | "<" | ">" | "@" | ";" | ":" | "\" | <"> | "/" | "[" | "]" | "?" | "{" | "}"]]>
        /// Excludes Percent "%": not considered safe because it is used in percent encoding
        /// Excludes Plus "+": not considered safe because its interpreted as space during decoding
        /// </summary>
        private static readonly char[] _allowedChars = { '!', '#', '$', '&', '\'', '*', '-', '.', '~', '^', '_', '`', '|', ',', '=' };

        /// <summary>
        /// Percent encodes non-alpha-numeric characters within the specified string
        /// excluding the symbols listed in <see cref="_allowedChars" /> using UTF-8
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EscapeString(string str)
        {
            if (str == null)
            {
                return null;
            }
            return Runtime.CustomPercentEscaper.PercentEncode(str, _allowedChars);
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
        /// excluding the symbols listed in <see cref="_allowedChars" /> using UTF-8
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public static IDictionary<string, string> EscapeMetadata(IDictionary<string, string> metadata)
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
        public static IDictionary<string, string> UnescapeMetadata(IDictionary<string, string> metadata)
        {
            return metadata.ToDictionary(
                data => UnescapeString(data.Key),
                data => UnescapeString(data.Value));
        }
    }
}
