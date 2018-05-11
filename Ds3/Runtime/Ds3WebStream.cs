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

using System.Diagnostics;
using System.IO;

namespace Ds3.Runtime
{
    public class Ds3WebStream : Stream
    {
        private static readonly TraceSwitch Log = new TraceSwitch("Ds3.Runtime", "set in config file");

        private readonly Stream _stream;
        private readonly long _contentLength;
        private long _bytesRead = 0;

        public Ds3WebStream(Stream stream, long contentLength)
        {
            if (Log.TraceVerbose && stream.CanSeek) { Trace.TraceInformation("Ds3WebStream of type {0} Position={1}, Length={2}", stream.GetType(), stream.Position, stream.Length); }
            _stream = stream;
            _contentLength = contentLength;
        }

        public override bool CanRead
        {
            get { return _stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _stream.CanWrite; }
        }

        public override long Length
        {
            get { return _stream.Length; }
        }

        public override long Position
        {
            get { return _stream.Position; }

            set { _stream.Position = value; }
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var watch = Stopwatch.StartNew();
            if (Log.TraceVerbose && _stream.CanSeek) { Trace.TraceInformation("Position before read={0}", _stream.Position); }
            var bytesRead = _stream.Read(buffer, offset, count);
            if (Log.TraceVerbose && _stream.CanSeek) { Trace.TraceInformation("Position after read={0}", _stream.Position); }
            watch.Stop();
            _bytesRead += bytesRead;

            if (Log.TraceVerbose) { Trace.TraceInformation("Read {0} bytes in {1} sec. [{2}/{3}]", bytesRead, watch.ElapsedMilliseconds / 1000D, _bytesRead, _contentLength); }

            if (_contentLength > -1 && (bytesRead == 0) && (_bytesRead != _contentLength))
                throw new Ds3ContentLengthNotMatch(Resources.ContentLengthNotMatch, _contentLength, _bytesRead);
            return bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        public long GetBytesRead()
        {
            return _bytesRead;
        }
    }
}