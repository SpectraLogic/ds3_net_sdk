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
using System.IO;

using Ds3.Runtime;

namespace Ds3.Calls
{
    public class GetObjectResponse : Ds3Response
    {
        private Stream _contents;

        /// <summary>
        /// The contents of the object. Disposing of GetObjectResponse will also dispose of this stream.
        /// </summary>
        public Stream Contents
        {
            get { return _contents; }
        }

        internal GetObjectResponse(IWebResponse responseStream) 
            : base(responseStream)
        {
            HandleStatusCode(HttpStatusCode.OK);
            ProcessResponse();   
        }

        private void ProcessResponse()
        {
            _contents = response.GetResponseStream();
        }
    }
}
