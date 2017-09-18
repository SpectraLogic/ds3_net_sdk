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

using Ds3;
using Ds3.Helpers;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace Ds3Examples
{
    class PartialGetExample
    {
        private static TraceSwitch clientSwitch = new TraceSwitch("clientSwitch", "Controls tracing for example client");

        static void Main(string[] args)
        {
            // Configure and build the core client.
            IDs3Client client = new Ds3Builder(
                ConfigurationManager.AppSettings["Ds3Endpoint"],
                new Credentials(
                    ConfigurationManager.AppSettings["Ds3AccessKey"],
                    ConfigurationManager.AppSettings["Ds3SecretKey"]
                )
            ).WithProxy(new Uri("http://localhost:8888")).Build();

            // Set up the high-level abstractions.
            IDs3ClientHelpers helpers = new Ds3ClientHelpers(client);

            string bucket = "sharon_test";
            string directory = @"C:\Users\sharons\Desktop\test";
            string filename = "elasticsearch-5.3.0.zip";

            var driveResult = FileSystemHelpers.Check(directory, helpers.ListObjects(bucket));
            switch (driveResult.Status)
            {
                case DriveStatus.NoPermission:
                case DriveStatus.NotEnoughSpace:
                case DriveStatus.DirectoryNotExists:
                case DriveStatus.Error:
                case DriveStatus.Unknown:
                    Console.WriteLine($"FileSystemHelpers check failed with [{driveResult.Status}, {driveResult.ErrorMessage}]");
                    break;
                case DriveStatus.Ok:

                    var partialObjects = new List<Ds3PartialObject>
                    {
                        new Ds3PartialObject(Range.ByPosition(0, 10), filename),
                        new Ds3PartialObject(Range.ByPosition(20, 30), filename)
                    };
                    IEnumerable<string> empty = new string[] { };

                    IPartialReadJob job = helpers.StartPartialReadJob(bucket, empty, partialObjects);

                    // Keep the job id around. This is useful for job recovery in the case of a failure.
                    Console.WriteLine("Job id {0} started.", job.JobId);

                    // Tracing example 
                    if (clientSwitch.TraceInfo) { Trace.WriteLine(string.Format("StartPartialReadJob({0})", bucket)); }
                    if (clientSwitch.TraceVerbose) { Trace.WriteLine(string.Format("dd files from: {0}", directory)); }

                    // Transfer all of the files.
                    job.Transfer(FileHelpers.BuildPartialFileGetter(directory));

                    // Wait for user input.
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
