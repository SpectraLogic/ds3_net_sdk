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
using Ds3.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.Streams
{
    [TestFixture]
    public class TestStreamTranslator
    {
        private static Encoding _encoding = new UTF8Encoding(false);

        [Test, Timeout(500)]
        public void StreamCanReadFromCorrectPlaces()
        {
            var translator = new Mock<IRangeTranslator<string, string>>(MockBehavior.Strict);
            translator
                .Setup(t => t.Translate(ItIsContextRange("a", 0L, 10L)))
                .Returns(new[] { ContextRange.Create(Range.ByLength(0L, 10L), "lowercase") });
            translator
                .Setup(t => t.Translate(ItIsContextRange("b", 0L, 10L)))
                .Returns(new[] { ContextRange.Create(Range.ByLength(10L, 10L), "lowercase") });
            translator
                .Setup(t => t.Translate(ItIsContextRange("c", 0L, 3L)))
                .Returns(new[] { ContextRange.Create(Range.ByLength(20L, 3L), "lowercase") });
            translator
                .Setup(t => t.Translate(ItIsContextRange("c", 3L, 7L)))
                .Returns(new[]
                {
                    ContextRange.Create(Range.ByLength(23L, 3L), "lowercase"),
                    ContextRange.Create(Range.ByLength(0L, 4L), "uppercase"),
                });
            translator
                .Setup(t => t.Translate(ItIsContextRange("d", 0L, 10L)))
                .Returns(new[] { ContextRange.Create(Range.ByLength(4L, 10L), "uppercase") });
            translator
                .Setup(t => t.Translate(ItIsContextRange("e", 0L, 10L)))
                .Returns(new[] { ContextRange.Create(Range.ByLength(14L, 10L), "uppercase") });
            translator
                .Setup(t => t.Translate(ItIsContextRange("f", 0L, 6L)))
                .Returns(new[]
                {
                    ContextRange.Create(Range.ByLength(24L, 2L), "uppercase"),
                    ContextRange.Create(Range.ByLength(0L, 4L), "numbers"),
                });
            translator
                .Setup(t => t.Translate(ItIsContextRange("f", 6L, 4L)))
                .Returns(new[] { ContextRange.Create(Range.ByLength(4L, 4L), "numbers") });
            translator
                .Setup(t => t.Translate(ItIsContextRange("g", 0L, 2L)))
                .Returns(new[] { ContextRange.Create(Range.ByLength(8L, 2L), "numbers") });
            var payloads = new[]
            {
                Tuple.Create("a", "abcdefghij"),
                Tuple.Create("b", "klmnopqrst"),
                Tuple.Create("c", "uvw"),
                Tuple.Create("c", "xyzABCD"),
                Tuple.Create("d", "EFGHIJKLMN"),
                Tuple.Create("e", "OPQRSTUVWX"),
                Tuple.Create("f", "YZ0123"),
                Tuple.Create("f", "4567"),
                Tuple.Create("g", "89"),
            };
            var streams = new Dictionary<string, MockStream>
            {
                { "lowercase", new MockStream("abcdefghijklmnopqrstuvwxyz") },
                { "uppercase", new MockStream("ABCDEFGHIJKLMNOPQRSTUVWXYZ") },
                { "numbers", new MockStream("0123456789") },
            };
            var results = new Queue<Tuple<string, string>>();
            using (var resourceStore = new ResourceStore<string, Stream>(key => streams[key]))
            {
                var buffer = new byte[100];
                var rand = new Random();
                foreach (var context in payloads.GroupBy(it => it.Item1, it => it.Item2.Length))
                {
                    using (var stream = new StreamTranslator<string, string>(translator.Object, resourceStore, context.Key, context.Sum(it => it)))
                    {
                        foreach (var payloadSize in context)
                        {
                            var byteIndex = rand.Next(40);
                            stream.Read(buffer, byteIndex, payloadSize);
                            results.Enqueue(Tuple.Create(context.Key, _encoding.GetString(buffer, byteIndex, payloadSize)));
                        }
                    }
                }
            }
            CollectionAssert.AreEqual(payloads, results.ToArray());
            CollectionAssert.AreEquivalent(new[] { 26, 26, 10 }, streams.Values.Select(str => str.Result.Length).ToArray());
        }

        [Test]
        public void StreamCanWriteToCorrectPlaces()
        {
            var translator = new Mock<IRangeTranslator<string, string>>(MockBehavior.Strict);
            var payloads = new[]
            {
                Tuple.Create("a", "abcdefghij"),
                Tuple.Create("b", "klmnopqrst"),
                Tuple.Create("c", "uvw"),
                Tuple.Create("c", "xyzABCD"),
                Tuple.Create("d", "EFGHIJKLMN"),
                Tuple.Create("e", "OPQRSTUVWX"),
                Tuple.Create("f", "YZ0123"),
                Tuple.Create("f", "4567"),
                Tuple.Create("g", "89"),
            }.GroupBy(it => it.Item1, it => it.Item2);
            translator
                .Setup(t => t.Translate(ItIsContextRange("a", 0L, 10L)))
                .Returns<ContextRange<string>>(cr => new[] { ContextRange.Create(Range.ByLength(0L, 10L), "lowercase") });
            translator
                .Setup(t => t.Translate(ItIsContextRange("b", 0L, 10L)))
                .Returns<ContextRange<string>>(cr => new[] { ContextRange.Create(Range.ByLength(10L, 10L), "lowercase") });
            translator
                .Setup(t => t.Translate(ItIsContextRange("c", 0L, 3L)))
                .Returns<ContextRange<string>>(cr => new[] { ContextRange.Create(Range.ByLength(20L, 3L), "lowercase") });
            translator
                .Setup(t => t.Translate(ItIsContextRange("c", 3L, 7L)))
                .Returns<ContextRange<string>>(cr => new[]
                {
                    ContextRange.Create(Range.ByLength(23L, 3L), "lowercase"),
                    ContextRange.Create(Range.ByLength(0L, 4L), "uppercase"),
                });
            translator
                .Setup(t => t.Translate(ItIsContextRange("d", 0L, 10L)))
                .Returns<ContextRange<string>>(cr => new[] { ContextRange.Create(Range.ByLength(4L, 10L), "uppercase"), });
            translator
                .Setup(t => t.Translate(ItIsContextRange("e", 0L, 10L)))
                .Returns<ContextRange<string>>(cr => new[] { ContextRange.Create(Range.ByLength(14L, 10L), "uppercase"), });
            translator
                .Setup(t => t.Translate(ItIsContextRange("f", 0L, 6L)))
                .Returns<ContextRange<string>>(cr => new[]
                {
                    ContextRange.Create(Range.ByLength(24L, 2L), "uppercase"),
                    ContextRange.Create(Range.ByLength(0L, 4L), "numbers"),
                });
            translator
                .Setup(t => t.Translate(ItIsContextRange("f", 6L, 4L)))
                .Returns<ContextRange<string>>(cr => new[] { ContextRange.Create(Range.ByLength(4L, 4L), "numbers") });
            translator
                .Setup(t => t.Translate(ItIsContextRange("g", 0L, 2L)))
                .Returns<ContextRange<string>>(cr => new[] { ContextRange.Create(Range.ByLength(8L, 2L), "numbers"), });
            var streams = new ConcurrentDictionary<string, MockStream>();
            using (var resourceStore = new ResourceStore<string, Stream>(key => streams.GetOrAdd(key, k => new MockStream())))
            {
                var buffer = new byte[100];
                var rand = new Random();
                foreach (var context in payloads)
                {
                    using (var stream = new StreamTranslator<string, string>(translator.Object, resourceStore, context.Key, context.Sum(it => it.Length)))
                    {
                        foreach (var payload in context)
                        {
                            var byteIndex = rand.Next(40);
                            _encoding.GetBytes(payload, 0, payload.Length, buffer, byteIndex);
                            stream.Write(buffer, byteIndex, payload.Length);
                        }
                    }
                }
            }
            CollectionAssert.AreEquivalent(
                new[]
                {
                    new { Key = "lowercase", Result = "abcdefghijklmnopqrstuvwxyz" },
                    new { Key = "uppercase", Result = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
                    new { Key = "numbers", Result = "0123456789" },
                },
                streams.Select(kvp => new { kvp.Key, Result = _encoding.GetString(kvp.Value.Result) })
            );
        }

        private static ContextRange<T> ItIsContextRange<T>(T context, long offset, long length)
            where T : IComparable<T>
        {
            return Match.Create(
                it => it.Context.Equals(context) && it.Range.Start == offset && it.Range.Length == length,
                () => ContextRange.Create(Range.ByLength(offset, length), context)
            );
        }
    }
}
