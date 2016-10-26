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

using Ds3.Calls;

namespace Ds3.Helpers.Transferrers
{
    internal class ReadTransferrer : ITransferrer
    {
        public void Transfer(TransferrerOptions transferrerOptions)
        {
            var response = transferrerOptions.Client.GetObject(new GetObjectRequest(
                transferrerOptions.BucketName, transferrerOptions.ObjectName, transferrerOptions.Stream, transferrerOptions.JobId, transferrerOptions.BlobOffset));
            if (transferrerOptions.BlobOffset == 0)
            {
                transferrerOptions.MetadataListener?.Invoke(transferrerOptions.ObjectName, MetadataUtils.GetUriUnEscapeMetadata(response.Metadata));
            }
        }
    }
}