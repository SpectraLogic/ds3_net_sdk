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
using System.IO;
using System.Reflection;
using Ds3.Models;

namespace IntegrationTestDs3WithProxy
{
    internal static class Utils
    {
        internal static readonly List<Ds3Object> Objects = new List<Ds3Object>
        {
            new Ds3Object("1_blob.txt", 8160373),
            new Ds3Object("2_blobs.txt", 18513074),
            new Ds3Object("3_blobs.txt", 28200744)
        };

        internal static Stream ReadResource(string resourceName)
        {
            return
                Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("IntegrationTestDs3WithProxy.TestData." + resourceName);
        }
    }
}