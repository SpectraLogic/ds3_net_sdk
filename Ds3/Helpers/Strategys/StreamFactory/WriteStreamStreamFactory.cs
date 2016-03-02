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
    internal class WriteStreamStreamFactory : IStreamFactory<string>
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
                    Console.WriteLine("[{0}] Restore a saved stream for {1}", Thread.CurrentThread.ManagedThreadId, blob.Context);
                    if (stream.Position != blob.Range.Start)
                    {
                        throw new Exception("Stream position does not match blob position");
                    }

                    Console.WriteLine("[{0}] setting the stream length {1}", Thread.CurrentThread.ManagedThreadId, length);
                    stream.SetLength(length);
                    return stream;
                }

                Console.WriteLine("[{0}] Create a stream for {1}", Thread.CurrentThread.ManagedThreadId, blob.Context);
                var innerStream = createStreamForTransferItem(blob.Context);
                if (innerStream.Position != blob.Range.Start)
                {
                    throw new Exception("Stream position does not match blob position");
                }
                stream = new PutObjectRequestStream(innerStream, length);
                this._streamStore.Add(blob.Context, stream);
                return stream;
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
                Console.WriteLine("[{0}] Closing the stream for {1}", Thread.CurrentThread.ManagedThreadId, item);
                Stream stream;
                if (!this._streamStore.TryGetValue(item, out stream))
                {
                    Console.WriteLine("[{0}] ERROR stream not found!", Thread.CurrentThread.ManagedThreadId);
                    throw new Exception("ERROR stream not found!");
                }

                stream.Close();
            }
        }
    }
}