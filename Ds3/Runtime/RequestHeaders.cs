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

namespace Ds3.Runtime
{
    class RequestHeaders : IRequestHeaders
    {
        private Dictionary<string, string> _headers = new Dictionary<string, string>();

        /// <summary>
        /// The percent encoded request headers dictionary
        /// </summary>
        public Dictionary<string, string> Headers
        {
            get { return _headers; }
        }

        /// <summary>
        /// Gets a collection containing the non-percent-encoded keys in request headers
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string>.KeyCollection Keys()
        {
            return MetadataUtil.UnescapeMetadata(_headers).Keys;
        }

        /// <summary>
        /// Removes the value with the specified key from request headers
        /// </summary>
        /// <param name="key">The non-percent-encoded value to be removed</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return _headers.Remove(MetadataUtil.EscapeString(key));
        }

        /// <summary>
        /// Percent encodes and adds a key-value-pair to request headers
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            _headers.Add(MetadataUtil.EscapeString(key), MetadataUtil.EscapeString(value));
        }
    }
}
