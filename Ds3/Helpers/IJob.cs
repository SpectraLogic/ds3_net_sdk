﻿/*
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
using System.Threading;

namespace Ds3.Helpers
{
    public interface IJob
    {
        Guid JobId { get; }
        string BucketName { get; }

        IJob WithMaxParallelRequests(int maxParallelRequests);
        IJob WithCancellationToken(CancellationToken cancellationToken);

        void Transfer(Func<string, Stream> createStreamForObjectKey);

        event Action<long> DataTransferred;
        event Action<string> ObjectCompleted;
    }
}