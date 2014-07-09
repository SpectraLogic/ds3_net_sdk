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
    internal class WindowedStream : Stream
    {
        private readonly Stream _innerStream;
        private readonly long _originalLength;
        private long _length;
        private readonly long _offset;
        private readonly ICriticalSectionExecutor _criticalSectionExecutor;

        public WindowedStream(Stream innerStream, ICriticalSectionExecutor criticalSectionExecutor, long offset, long length)
        {
            if (!innerStream.CanSeek)
            {
                throw new ArgumentException(Resources.CannotSeekStreamException);
            }

            this._innerStream = innerStream;
            this._criticalSectionExecutor = criticalSectionExecutor;
            this._length = this._originalLength = length;
            this._offset = offset;
        }

        public override bool CanRead
        {
            get { return this._innerStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return this._innerStream.CanWrite; }
        }

        public override void Flush()
        {
            this._criticalSectionExecutor.InLock(this._innerStream.Flush);
        }

        public override long Length
        {
            get { return this._length; }
        }

        public override long Position { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this._criticalSectionExecutor.InLock(delegate
            {
                this._innerStream.Position = this.Position + this._offset;
                var bytesRead = this._innerStream.Read(buffer, offset, (int)Math.Min(this.Length - this.Position, count));
                this.Position += bytesRead;
                return bytesRead;
            });
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.Position = offset;
                    break;
                case SeekOrigin.Current:
                    this.Position += offset;
                    break;
                case SeekOrigin.End:
                    this.Position = this._length - offset;
                    break;
                default:
                    throw new NotSupportedException(string.Format(Resources.InvalidSeekOrigin, origin));
            }
            return this.Position;
        }

        public override void SetLength(long value)
        {
            this._length = Math.Min(value, this._originalLength);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this._criticalSectionExecutor.InLock(delegate
            {
                this._innerStream.Position = this.Position + this._offset;
                this._innerStream.Write(buffer, offset, count);
                this.Position += count;
            });
        }
    }
}
