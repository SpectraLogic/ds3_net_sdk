using System;
using System.IO;

namespace Ds3.Helpers
{
    internal class StreamWindowFactory : IDisposable
    {
        private bool _disposed;
        private readonly Stream _stream;
        private readonly ICriticalSectionExecutor _criticalSectionExecutor = new CriticalSectionExecutor();

        public StreamWindowFactory(Stream stream)
        {
            this._stream = stream;
        }

        public Stream Get(long offset, long length)
        {
            return new WindowedStream(this._stream, this._criticalSectionExecutor, offset, length);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                this._stream.Dispose();
            }

            this._disposed = true;
        }
    }
}
