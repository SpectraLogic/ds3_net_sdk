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

namespace Ds3.Models
{
    public class Node
    {
        public Guid Id { get; private set; }
        public string EndPoint { get; private set; }
        public int? HttpPort { get; private set; }
        public int? HttpsPort { get; private set; }

        public Node(Guid id, string endPoint, int? httpPort, int? httpsPort)
        {
            this.Id = id;
            this.EndPoint = endPoint;
            this.HttpPort = httpPort;
            this.HttpsPort = httpsPort;
        }
    }
}
