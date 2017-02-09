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

using System.IO;
using System.Text;

namespace TestDs3.Helpers
{
    internal class MockStream : MemoryStream
    {
        public MockStream()
        {
        }

        public MockStream(string data)
        {
            var buffer = new UTF8Encoding(false).GetBytes(data);
            Write(buffer, 0, buffer.Length);
            Position = 0L;
        }

        public byte[] Result { get; private set; }

        protected override void Dispose(bool disposing)
        {
            Result = ToArray();
            base.Dispose(disposing);
        }
    }
}