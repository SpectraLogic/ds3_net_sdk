using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ds3.Helpers.RangeTranslators;
using Ds3.Helpers.Streams;

namespace Ds3.Helpers.Strategies.StreamFactory
{
    public class StreamFactory : IStreamFactory<string>
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
