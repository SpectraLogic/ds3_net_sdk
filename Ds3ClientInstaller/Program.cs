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
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Ds3ClientInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Ds3ClientInstaller.Ds3Client.zip"))
            using (var zip = new ZipArchive(stream))
            {
                var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var moduleDirectory = Path.Combine(myDocs, @"WindowsPowerShell\Modules\Ds3Client");
                if (Directory.Exists(moduleDirectory))
                {
                    Directory.Delete(moduleDirectory, true);
                }
                zip.ExtractToDirectory(moduleDirectory);
            }
        }
    }
}
