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
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ds3.Helpers.Streams
{
    internal class StreamTranslator<TInnerContext, TOuterContext> : Stream
        where TInnerContext : IComparable<TInnerContext>
        where TOuterContext : IComparable<TOuterContext>
    {
        private bool _disposed = false;
        private readonly ISet<TInnerContext> _writtenContexts = new HashSet<TInnerContext>();
        private readonly IRangeTranslator<TOuterContext, TInnerContext> _rangeTranslator;
        private readonly IResourceStore<TInnerContext, Stream> _streamStore;
        private readonly TOuterContext _outerContext;
        private long _streamLength;

        public StreamTranslator(
            IRangeTranslator<TOuterContext, TInnerContext> rangeTranslator,
            IResourceStore<TInnerContext, Stream> streamStore,
            TOuterContext outerContext,
            long streamLength)
        {
            this._rangeTranslator = rangeTranslator;
            this._streamStore = streamStore;
            this._outerContext = outerContext;
            this._streamLength = streamLength;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            foreach (var context in this._writtenContexts)
            {
                this._streamStore.Access(context, stream => stream.Flush());
            }
        }

        public override long Length { get { return this._streamLength; } }

        public override long Position { get; set; }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin: return this.Position = offset;
                case SeekOrigin.Current: return this.Position += offset;
                case SeekOrigin.End: return this.Position = this.Length - offset;
                default: throw new InvalidOperationException();
            }
        }

        public override void SetLength(long value)
        {
            this._streamLength = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var contextRange = ContextRange.Create(Range.ByLength(this.Position, count), this._outerContext);
            var progress = 0;
            foreach (var range in this._rangeTranslator.Translate(contextRange))
            {
                // We know that every range length <= count.
                var rangeLength = (int)range.Range.Length;
                this._streamStore.Access(range.Context, stream =>
                {
                    stream.Position = range.Range.Start;
                    for (var streamProgress = 0; streamProgress < rangeLength; )
                    {
                        streamProgress += stream.Read(
                            buffer,
                            offset + progress + streamProgress,
                            rangeLength - streamProgress
                        );
                    }
                });
                progress += rangeLength;
            }
            this.Position += progress;
            return progress;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var contextRange = ContextRange.Create(Range.ByLength(this.Position, count), this._outerContext);
            var progress = 0;
            foreach (var range in this._rangeTranslator.Translate(contextRange))
            {
                // We know that every range length <= count.
                var rangeLength = (int)range.Range.Length;
                this._writtenContexts.Add(range.Context);
                this._streamStore.Access(range.Context, stream =>
                {
                    stream.Position = range.Range.Start;
                    stream.Write(buffer, progress + offset, rangeLength);
                });
                progress += rangeLength;
            }
            this.Position += count;
        }

        private void TranslateOperation(int count, Action<Stream, int, int> performIo)
        {
            var contextRange = ContextRange.Create(Range.ByLength(this.Position, count), this._outerContext);
            var progress = 0;
            foreach (var range in this._rangeTranslator.Translate(contextRange))
            {
                // We know that every range length <= count.
                var rangeLength = (int)range.Range.Length;
                this._writtenContexts.Add(range.Context);
                this._streamStore.Access(range.Context, stream =>
                {
                    stream.Position = range.Range.Start;
                    performIo(stream, progress, rangeLength);
                });
                progress += rangeLength;
            }
            this.Position += count;
        }

        protected override void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }
            if (disposing)
            {
                this.Flush();
            }
            this._disposed = true;
            base.Dispose(disposing);
        }
    }
}
