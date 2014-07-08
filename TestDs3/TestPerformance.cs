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
using System.Diagnostics;
using System.IO;
using System.Linq;

using NUnit.Framework;

using Ds3.Helpers;
using Ds3.Runtime;

namespace TestDs3
{
    [TestFixture, Explicit("Long running test checks functionality rather than performance.")]
    public class TestPerformance
    {
        private const int _defaultBufferSize = 80 * 1024;

        [Test]
        public void TestReadFileStreamPerformance()
        {
            RunReadFileStreamPerformanceTest(CreateRandomFile(), 12, Network.DefaultCopyBufferSize);
        }

        private static void RunReadFileStreamPerformanceTest(string testFileName, int windowedStreamCount, int bufferSize)
        {
            var criticalSectionExecutor = new CriticalSectionExecutor();
            using (var file = File.OpenRead(testFileName))
            {
                var sectionSize = file.Length / windowedStreamCount;
                Console.WriteLine("Creating {0} windowed streams of size {1} each.", windowedStreamCount, sectionSize);

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                Enumerable
                    .Range(0, windowedStreamCount)
                    .Select(i => new WindowedStream(file, criticalSectionExecutor, i * sectionSize, sectionSize))
                    .AsParallel()
                    .ForAll(stream => stream.CopyTo(Stream.Null, bufferSize));
                stopwatch.Stop();

                Console.WriteLine(
                    "Read {0} bytes in {1} at a rate of {2}bps.",
                    file.Length,
                    stopwatch.Elapsed,
                    new TimeSpan(10000000 * file.Length / stopwatch.Elapsed.Ticks)
                );
            }
        }

        private static string CreateRandomFile()
        {
            var testFileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestWindowedStreamFile");
            if (!File.Exists(testFileName))
            {
                Console.WriteLine("5 gig test file does not exist. Creating...");
                CreateRandomFile(testFileName, 5L * 1024L * 1024L * 1024L);
                Console.WriteLine("Finished writing 5 gig file.");
            }
            return testFileName;
        }

        private static void CreateRandomFile(string testFileName, long testFileSize)
        {
            using (var testFile = File.Create(testFileName, _defaultBufferSize, FileOptions.WriteThrough))
            {
                var rand = new Random();
                var buffer = new byte[_defaultBufferSize];
                for (var i = 0; i < testFileSize / _defaultBufferSize; i++)
                {
                    rand.NextBytes(buffer);
                    testFile.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
