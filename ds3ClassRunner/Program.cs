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

namespace ds3ClassRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Ds3Builder("http://192.168.56.101:18080", new Credentials("c3BlY3RyYQ==", "sUEnvVsZ"))
                .WithProxy(new Uri("http://192.168.56.101:8888"))
                .Build();
            var helpers = new Ds3ClientHelpers(client);
        }
    }
}
