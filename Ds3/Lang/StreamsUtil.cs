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

namespace Ds3.Lang
{
    public static class StreamsUtil
    {
        public static void BufferedCopyTo(Stream src, Stream dst, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            var totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = src.Read(buffer, totalBytesRead, bufferSize - totalBytesRead)) != 0)
            {
                totalBytesRead += bytesRead;
                if (totalBytesRead != bufferSize) continue;
                dst.Write(buffer, 0, totalBytesRead);
                totalBytesRead = 0;
            }

            if (totalBytesRead > 0)
            {
                dst.Write(buffer, 0, totalBytesRead);
            }
        }
    }
}