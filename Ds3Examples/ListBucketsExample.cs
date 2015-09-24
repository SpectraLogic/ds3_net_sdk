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
using Ds3.Calls;
using System;
using System.Configuration;
using System.Diagnostics;

namespace Ds3Examples
{
    /// <summary>
    /// Shows how to list all of the buckets on DS3 server.
    ///
    /// There aren't really any gotchas to this, so we use the core client directly.
    /// </summary>
    class ListBucketsExample
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

            // Tracing example 
            if (clientSwitch.TraceInfo) { Trace.WriteLine("List all buckets"); }

            // Loop through all of the objects in the bucket.
            foreach (var bucket in client.GetService(new GetServiceRequest()).Buckets)
            {
                if (clientSwitch.TraceVerbose) { Trace.WriteLine(string.Format("Bucket '{0}'.", bucket.Name)); }
                Console.WriteLine("Bucket '{0}'.", bucket.Name);
            }

            // Wait for user input.
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }
    }
}
