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

using Ds3.Helpers.RangeTranslators;
using Ds3.Helpers.Streams;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ds3.Helpers.Strategies.StreamFactory
{
    /// <summary>
    /// Create a stream for each blob and transfer blobs in parallel
    /// </summary>
    public class WriteRandomAccessStreamFactory : IStreamFactory<string>
    {
        private readonly object _lock = new object();
        private readonly Dictionary<Blob, Stream> _streamStore = new Dictionary<Blob, Stream>();

        public Stream CreateStream(Func<string, Stream> createStreamForTransferItem, IRangeTranslator<Blob, string> rangeTranslator, Blob blob, long length)
        {
            lock (this._lock)
            {
                Stream stream;
                /* if the blob length is bigger than the copy buffer size we want to reuse the stream for that blob */
                if (this._streamStore.TryGetValue(blob, out stream))
                {
                    return stream;
                }

                // Create a new stream for the transfered blob
                stream = new NonDisposablePutObjectRequestStream(createStreamForTransferItem(blob.Context), blob.Range.Start, length);

                this._streamStore.Add(blob, stream);
                return stream;
            }
        }

        public void CloseBlob(Blob blob)
        {
            lock (_lock)
            {
                Stream stream;
                if (!this._streamStore.TryGetValue(blob, out stream))
                {
                    throw new StreamNotFoundException(string.Format("Stream not found for blob ({0}, {1}:{2})", blob.Context, blob.Range.Start, blob.Range.End));
                }

                ((NonDisposablePutObjectRequestStream)stream).DisposeUnderlineStream();
                this._streamStore.Remove(blob);
            }
        }

        public void CloseStream(string item)
        {
            //no stream to close with this implementation
        }
    }
}