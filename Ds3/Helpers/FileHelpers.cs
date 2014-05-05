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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ObjectGetter = Ds3.Helpers.Ds3ClientHelpers.ObjectGetter;
using ObjectPutter = Ds3.Helpers.Ds3ClientHelpers.ObjectPutter;

namespace Ds3.Helpers
{
    public class FileHelpers
    {
        public static ObjectGetter BuildFileGetter(string root)
        {
            return (ds3Client, contents) =>
            {
                using (var outputFile = File.OpenWrite(EnsureDirectoryForFile(Path.Combine(root, ds3Client.Name))))
                {
                    contents.CopyTo(outputFile);
                }
            };
        }

        private static string EnsureDirectoryForFile(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return path;
        }

        public static ObjectPutter BuildFilePutter(string root)
        {
            return ds3Client => File.OpenRead(Path.Combine(root, ds3Client.Name));
        }
    }
}
