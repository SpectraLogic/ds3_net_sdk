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

namespace Ds3.Helpers.Strategys.StreamFactory
{
    /// <summary>
    /// Create one stream and transfer blobs in order without seeking
    /// </summary>
    public class WriteStreamStreamFactory : IStreamFactory<string>
    {
        private readonly object _lock = new object();
        private readonly Dictionary<string, Stream> _streamStore = new Dictionary<string, Stream>();

        public Stream CreateStream(Func<string, Stream> createStreamForTransferItem, IRangeTranslator<Blob, string> rangeTranslator, Blob blob, long length)
        {
            lock (_lock)
            {
                Stream stream;
                if (this._streamStore.TryGetValue(blob.Context, out stream))
                {
                    return new NonDisposablePutObjectRequestStream(stream, length);
                }

                var innerStream = createStreamForTransferItem(blob.Context);

                this._streamStore.Add(blob.Context, innerStream);
                return new NonDisposablePutObjectRequestStream(innerStream, length);
            }
        }

        public void CloseBlob(Blob blob)
        {
            //no blobs to close
        }

        public void CloseStream(string item)
        {
            lock (this._lock)
            {
                Stream stream;
                if (!this._streamStore.TryGetValue(item, out stream))
                {
                    throw new StreamNotFoundException(string.Format("Stream not found for {0}", item));
                }

                stream.Close();
                this._streamStore.Remove(item);
            }
        }

        public Dictionary<string, Stream> GetStreamStore()
        {
            return _streamStore;
        }
    }
}