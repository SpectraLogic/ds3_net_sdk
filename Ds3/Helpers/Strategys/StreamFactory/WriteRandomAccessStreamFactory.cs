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
using System.Threading;

namespace Ds3.Helpers.Strategys.StreamFactory
{
    internal class WriteRandomAccessStreamFactory : IStreamFactory<string>
    {
        private readonly object _lock = new object();
        private readonly Dictionary<Blob, Stream> _streamStore = new Dictionary<Blob, Stream>();

        public Stream CreateStream(Func<string, Stream> createStreamForTransferItem, IRangeTranslator<Blob, string> rangeTranslator, Blob blob, long length)
        {
            lock (this._lock)
            {
                Stream stream;
                if (this._streamStore.TryGetValue(blob, out stream)) //in case the blob length is bigger than the copy buffer size
                {
                    Console.WriteLine("[{0}] Restore a saved stream for {1} blob [{2}:{3}=>{4}]", Thread.CurrentThread.ManagedThreadId, blob.Context, blob.Range.Start, blob.Range.End, blob.Range.Length);
                    return stream;
                }

                Console.WriteLine("[{0}] Create a stream for {1} blob [{2}:{3}=>{4}]", Thread.CurrentThread.ManagedThreadId, blob.Context, blob.Range.Start, blob.Range.End, blob.Range.Length);
                stream = new PutObjectRequestStream(createStreamForTransferItem(blob.Context), blob.Range.Start, length);

                this._streamStore.Add(blob, stream);
                return stream;
            }
        }

        public void CloseBlob(Blob blob)
        {
            lock (_lock)
            {
                Console.WriteLine("[{0}] Closing the stream for {1} blob start {2}", Thread.CurrentThread.ManagedThreadId, blob.Context, blob.Range.Start);
                Stream stream;
                if (!this._streamStore.TryGetValue(blob, out stream))
                {
                    Console.WriteLine("[{0}] ERROR stream not found!", Thread.CurrentThread.ManagedThreadId);
                    throw new Exception("ERROR stream not found!");
                }

                stream.Close();
            }
        }

        public void CloseFile(string file)
        {
            //no file to close with this implementation
        }
    }
}