/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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

// This code is auto-generated, do not modify
using Ds3.Models;
using System;
using System.Net;

namespace Ds3.Calls
{
    public class PutTapeDensityDirectiveSpectraS3Request : Ds3Request
    {
        
        public TapeDriveType Density { get; private set; }

        public string PartitionId { get; private set; }

        public string TapeType { get; private set; }

        

        
        
        public PutTapeDensityDirectiveSpectraS3Request(TapeDriveType density, Guid partitionId, string tapeType)
        {
            this.Density = density;
            this.PartitionId = partitionId.ToString();
            this.TapeType = tapeType;
            
            this.QueryParams.Add("density", density.ToString());

            this.QueryParams.Add("partition_id", partitionId.ToString());

            this.QueryParams.Add("tape_type", tapeType);

        }

        
        public PutTapeDensityDirectiveSpectraS3Request(TapeDriveType density, string partitionId, string tapeType)
        {
            this.Density = density;
            this.PartitionId = partitionId;
            this.TapeType = tapeType;
            
            this.QueryParams.Add("density", density.ToString());

            this.QueryParams.Add("partition_id", partitionId);

            this.QueryParams.Add("tape_type", tapeType);

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.POST;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/tape_density_directive";
            }
        }
    }
}