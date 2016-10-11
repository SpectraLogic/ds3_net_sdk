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

using Ds3.Models;

namespace TestDs3.Helpers.Strategies.StreamFactory
{
    public static class BlobsStub
    {
        public const int Blob1Length = 15;
        public const int Blob2Length = 10;
        public const int Blob3Length = 20;
        public static readonly Ds3.Helpers.Blob Blob1 = new Ds3.Helpers.Blob(Range.ByLength(0, 15), "bar");
        public static readonly Ds3.Helpers.Blob Blob2 = new Ds3.Helpers.Blob(Range.ByLength(15, 10), "bar");
        public static readonly Ds3.Helpers.Blob Blob3 = new Ds3.Helpers.Blob(Range.ByLength(0, 20), "foo");
    }
}