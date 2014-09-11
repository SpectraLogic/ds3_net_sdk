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
