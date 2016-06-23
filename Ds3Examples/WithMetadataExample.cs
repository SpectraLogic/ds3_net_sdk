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
using System.Configuration;
using System.Diagnostics;
using Ds3;
using Ds3.Helpers;

namespace Ds3Examples
{
    class WithMetadataExample
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
                ).Build();

            // Set up the high-level abstractions.
            IDs3ClientHelpers helpers = new Ds3ClientHelpers(client);

            string bucket = "bucket-name";
            string directory = "TestData";

            // Creates a bucket if it does not already exist.
            helpers.EnsureBucketExists(bucket);
            if (clientSwitch.TraceVerbose)
            {
                Trace.WriteLine(string.Format("Bucket exists: {0}", bucket));
            }

            // Creates a bulk job with the server based on the files in a directory (recursively).
            IJob putJob = helpers.StartWriteJob(bucket, FileHelpers.ListObjectsForDirectory(directory));

            // Tracing example 
            if (clientSwitch.TraceInfo)
            {
                Trace.WriteLine(string.Format("StartWriteJob({0})", bucket));
            }
            if (clientSwitch.TraceVerbose)
            {
                Trace.WriteLine(string.Format("Add files from: {0}", directory));
            }

            // Keep the job id around. This is useful for job recovery in the case of a failure.
            Console.WriteLine("Job id {0} started.", putJob.JobId);

            // Add meta-data to objects transfered
            putJob.WithMetadata(new MetadataAccess());

            // Transfer all of the files.
            putJob.Transfer(FileHelpers.BuildFilePutter(directory));


            IJob getJob = helpers.StartReadAllJob(bucket);

            // Add Metadata listener
            getJob.MetadataListener += (fileName, metadata) =>
            {
                Trace.WriteLine(string.Format("Metadata for file {0}", fileName));
                foreach (var pair in metadata)
                {
                    Trace.WriteLine(string.Format("<{0}, {1}>", pair.Key, pair.Value));
                }
            };

            getJob.Transfer(FileHelpers.BuildFileGetter(directory));


            // Wait for user input.
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }
    }

    internal class MetadataAccess : IMetadataAccess
    {
        public IDictionary<string, string> GetMetadataValue(string filename)
        {
            return new Dictionary<string, string> {{"name", filename}};
        }
    }
}
