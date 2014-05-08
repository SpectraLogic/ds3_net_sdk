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

using System.Net;

using Ds3.Runtime;

namespace Ds3.Calls
{
    public class HeadBucketResponse : Ds3Response
    {
        internal HeadBucketResponse(IWebResponse response)
            : base(response)
        {
        }

        public StatusType Status { get; private set; }

        protected override void ProcessResponse()
        {
            HandleStatusCode(HttpStatusCode.OK, HttpStatusCode.Forbidden, HttpStatusCode.NotFound);
            switch (this.response.StatusCode)
            {
                case HttpStatusCode.OK: this.Status = StatusType.Exists; break;
                case HttpStatusCode.Forbidden: this.Status = StatusType.NotAuthorized; break;
                case HttpStatusCode.NotFound: this.Status = StatusType.DoesntExist; break;
            }
        }

        public enum StatusType
        {
            Exists,
            NotAuthorized,
            DoesntExist
        }
    }
}
