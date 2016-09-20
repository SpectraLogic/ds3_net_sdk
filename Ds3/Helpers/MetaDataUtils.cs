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

namespace Ds3.Helpers
{
    public static class MetadataUtils
    {
        public static IDictionary<string, string> GetUriEscapeMetadata(IDictionary<string, string> metadata)
        {
            return metadata.ToDictionary(
                data => Uri.EscapeDataString(data.Key),
                data => Uri.EscapeDataString(data.Value));
        }

        public static IDictionary<string, string> GetUriUnEscapeMetadata(IDictionary<string, string> metadata)
        {
            return metadata.ToDictionary(
                data => Uri.UnescapeDataString(data.Key),
                data => Uri.UnescapeDataString(data.Value));
        }
    }
}
