/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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

namespace Ds3.Helpers.Streams
{
    public class ObjectRequestStream : Stream, IDisposable
    {
        private readonly Stream _stream;
        private long _streamLength;
        private long _totalBytesRead = 0;
        private bool _disposed;
        private long _streamStartPoint;

        public ObjectRequestStream(Stream stream, long length)
        {
            this._stream = stream;
            this._streamLength = length;
        }

        public ObjectRequestStream(Stream stream, long offset, long length)
        {
            this._stream = stream;
           _streamStartPoint = offset;
            if (CanSeek) this._stream.Position = offset;
            this._streamLength = length;
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _stream.Dispose();
            }

            _disposed = true;
        }

        public override bool CanRead
        {
            get
            {
                return _stream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return _stream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override long Length
        {
            get
            {
                return _streamLength;
            }
        }

        public override long Position
        {
            get
            {
                return _stream.Position;
            }

            set
            {
                _stream.Position = value;
                _totalBytesRead = 0;
            }
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(_streamStartPoint + offset, origin);
        }

        public override void SetLength(long value)
        {
            this._streamLength = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_totalBytesRead == this._streamLength)
            {
                this._totalBytesRead = 0;
                return 0;
            }

            if (_totalBytesRead + count > this._streamLength)
            {
                count = (int) (this._streamLength - _totalBytesRead);
            }

            var bytesRead = _stream.Read(buffer, offset, count);
            _totalBytesRead += bytesRead;
            return bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        // Used for testing
        public long GetTotalBytesRead()
        {
            return _totalBytesRead;
        }
    }
}