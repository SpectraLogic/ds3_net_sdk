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

using Ds3.Helpers.RangeTranslators;
using Ds3.Helpers.Streams;
using System;
using System.IO;

namespace Ds3.Helpers.Strategies.StreamFactory
{
    /// <summary>
    /// Create one stream and receiving blobs in parallel using stream seeking
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class ReadRandomAccessStreamFactory<TItem> : IStreamFactory<TItem>
                where TItem : IComparable<TItem>

    {
        private readonly object _lock = new object();
        private IResourceStore<TItem, Stream> _resourceStore;

        public Stream CreateStream(Func<TItem, Stream> createStreamForTransferItem, IRangeTranslator<Blob, TItem> rangeTranslator, Blob blob, long length)
        {
            //create a singleton instance of the resource store
            lock (_lock)
            {
                if (_resourceStore == null)
                {
                    _resourceStore = new ResourceStore<TItem, Stream>(createStreamForTransferItem);
                }
            }

            return new StreamTranslator<TItem, Blob>(
                        rangeTranslator,
                        this._resourceStore,
                        blob,
                        length
            );
        }

        public void CloseBlob(Blob blob)
        {
            // no blobs to close with this implementation
        }

        public void CloseStream(TItem item)
        {
            _resourceStore.Close(item);
        }
    }
}