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

using System;
using System.IO;
using Ds3.Helpers.RangeTranslators;

namespace Ds3.Helpers.Strategies.StreamFactory
{
    public class ReadStreamStreamFactory : IStreamFactory<string>
    {
        public Stream CreateStream(Func<string, Stream> createStreamForTransferItem, IRangeTranslator<Blob, string> rangeTranslator, Blob blob, long length)
        {
            throw new NotImplementedException();
        }

        public void CloseBlob(Blob blob)
        {
            throw new NotImplementedException();
        }

        public void CloseStream(string item)
        {
            throw new NotImplementedException();
        }
    }
}
