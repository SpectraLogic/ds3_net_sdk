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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Ds3.Helpers.Strategies;

namespace Ds3Examples
{
    public class BulkPutWithStreamStrategyExample
    {
        private static readonly TraceSwitch ClientSwitch = new TraceSwitch("clientSwitch", "Controls tracing for example client");

        public static void Main()
        {
            // Configure and build the core client.
            var client = new Ds3Builder(
                ConfigurationManager.AppSettings["Ds3Endpoint"],
                new Credentials(
                    ConfigurationManager.AppSettings["Ds3AccessKey"],
                    ConfigurationManager.AppSettings["Ds3SecretKey"]
                )
            ).Build();

            // Set up the high-level abstractions.
            IDs3ClientHelpers helpers = new Ds3ClientHelpers(client);

            const string bucketName = "BulkPutWithStreamStrategy";
            const string directory = "TestData";

            // Creates a bucket if it does not already exist.
            helpers.EnsureBucketExists(bucketName);
            if (ClientSwitch.TraceVerbose) { Trace.WriteLine(string.Format("Bucket exists: {0}", bucketName)); }

            // Creates a bulk job with the server based on the files in a directory (recursively).
            var directoryObjects = FileHelpers.ListObjectsForDirectory(directory).ToList();
            var job = helpers.StartWriteJob(bucketName, directoryObjects, helperStrategy: new WriteStreamHelperStrategy());

            // Tracing example
            if (ClientSwitch.TraceInfo) { Trace.WriteLine(string.Format("StartWriteJob({0})", bucketName)); }
            if (ClientSwitch.TraceVerbose) { Trace.WriteLine(string.Format("Add files from: {0}", directory)); }

            // Keep the job id around. This is useful for job recovery in the case of a failure.
            Console.WriteLine("Job id {0} started.", job.JobId);

            var cryptoStreams = new Dictionary<string, CryptoStream>();
            var md5s = new Dictionary<string, MD5>();

            directoryObjects.ForEach(obj =>
            {
                var md5 = MD5.Create();
                var fileStream = File.OpenRead(Path.Combine(directory, obj.Name));
                var md5Stream = new CryptoStream(fileStream, md5, CryptoStreamMode.Read);

                cryptoStreams.Add(obj.Name, md5Stream);
                md5s.Add(obj.Name, md5);
            });

            // Transfer all of the files.
            job.Transfer(fileName => cryptoStreams[fileName]);

            foreach (var stream in cryptoStreams.Select(pair => pair.Value).Where(stream => !stream.HasFlushedFinalBlock))
            {
                stream.FlushFinalBlock();
            }

            foreach (var md5 in md5s)
            {
                Console.WriteLine("Done transferring file {0} with MD5 value {1}", md5.Key, Convert.ToBase64String(md5.Value.Hash));
            }

            // Wait for user input.
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }
    }
}