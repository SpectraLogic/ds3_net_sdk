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

using Ds3;
using Ds3.Helpers;
using System;
using System.Configuration;
using System.Diagnostics;

namespace Ds3Examples
{
    /// <summary>
    /// Shows how to do a bulk get of all of the objects in a bucket
    /// using our bulk job helpers along with the file helpers.
    /// </summary>
    class BulkGetExample
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
            string directory = "DataFromBucket";

            // Creates a bulk job with all of the objects in the bucket.
            IJob job = helpers.StartReadAllJob(bucket);
            // Same as: IJob job = helpers.StartReadJob(bucket, helpers.ListObjects(bucket));

            // Keep the job id around. This is useful for job recovery in the case of a failure.
            Console.WriteLine("Job id {0} started.", job.JobId);

            // Tracing example 
            if (clientSwitch.TraceInfo) { Trace.WriteLine(string.Format("StartReadAllJob({0})", bucket)); }
            if (clientSwitch.TraceVerbose) { Trace.WriteLine(string.Format("dd files from: {0}", directory)); }

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFileGetter(directory));

            // Wait for user input.
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }
    }
}
