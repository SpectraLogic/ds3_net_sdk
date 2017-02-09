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
    public class PutS3TargetReadPreferenceSpectraS3Request : Ds3Request
    {
        
        public string BucketId { get; private set; }

        public TargetReadPreferenceType ReadPreference { get; private set; }

        public string TargetId { get; private set; }

        

        
        
        public PutS3TargetReadPreferenceSpectraS3Request(Guid bucketId, TargetReadPreferenceType readPreference, Guid targetId)
        {
            this.BucketId = bucketId.ToString();
            this.ReadPreference = readPreference;
            this.TargetId = targetId.ToString();
            
            this.QueryParams.Add("bucket_id", bucketId.ToString());

            this.QueryParams.Add("read_preference", readPreference.ToString());

            this.QueryParams.Add("target_id", targetId.ToString());

        }

        
        public PutS3TargetReadPreferenceSpectraS3Request(string bucketId, TargetReadPreferenceType readPreference, string targetId)
        {
            this.BucketId = bucketId;
            this.ReadPreference = readPreference;
            this.TargetId = targetId;
            
            this.QueryParams.Add("bucket_id", bucketId);

            this.QueryParams.Add("read_preference", readPreference.ToString());

            this.QueryParams.Add("target_id", targetId);

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
                return "/_rest_/s3_target_read_preference";
            }
        }
    }
}