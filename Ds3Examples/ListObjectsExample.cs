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
    /// Shows how to list all objects for a bucket.
    /// The helper method handles the 1,000 object request paging logic.
    /// </summary>
    class ListObjectsExample
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

            string bucket = "bucket-name";

            // Set up the high-level abstractions.
            IDs3ClientHelpers helpers = new Ds3ClientHelpers(client);

            // Tracing example 
            if (clientSwitch.TraceInfo) { Trace.WriteLine(string.Format("ListObjects from bucket = {0}", bucket)); }

            // Loop through all of the objects in the bucket.
            foreach (var obj in helpers.ListObjects("bucket-name"))
            {
                if (clientSwitch.TraceVerbose) { Trace.WriteLine(string.Format("Object '{0}' of size {1}.", obj.Name, obj.Size)); }
                Console.WriteLine("Object '{0}' of size {1}.", obj.Name, obj.Size);
            }

            // Wait for user input.
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }
    }
}
