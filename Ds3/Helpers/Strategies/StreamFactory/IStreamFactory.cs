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
using System;
using System.IO;

namespace Ds3.Helpers.Strategies.StreamFactory
{
    public interface IStreamFactory<TItem> where TItem : IComparable<TItem>
    {
        /// <summary>
        /// Create a new stream for every blob returned by GetNextTransferItems
        /// </summary>
        /// <param name="createStreamForTransferItem"></param>
        /// <param name="rangeTranslator"></param>
        /// <param name="blob"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Stream CreateStream(Func<TItem, Stream> createStreamForTransferItem, IRangeTranslator<Blob, TItem> rangeTranslator, Blob blob, long length);

        /// <summary>
        /// If a blob to stream strategy has been chosen than close the stream that associated with the completed blob
        /// </summary>
        /// <param name="blob"></param>
        void CloseBlob(Blob blob);

        /// <summary>
        /// If a stream to stream strategy has been chosen than close the stream that associated with the completed stream
        /// </summary>
        /// <param name="item"></param>
        void CloseStream(TItem item);
    }
}