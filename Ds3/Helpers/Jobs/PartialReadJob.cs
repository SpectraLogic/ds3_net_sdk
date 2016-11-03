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

using Ds3.Calls;
using Ds3.Helpers.RangeTranslators;
using Ds3.Helpers.Strategies;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Ds3.Helpers.TransferStrategies;

namespace Ds3.Helpers.Jobs
{
    internal class PartialReadJob : Job<IPartialReadJob, Ds3PartialObject>, IPartialReadJob
    {
        public static IPartialReadJob Create(
            IDs3Client client,
            MasterObjectList jobResponse,
            IEnumerable<string> fullObjects,
            IEnumerable<Ds3PartialObject> partialObjects,
            IHelperStrategy<Ds3PartialObject> helperStrategy,
            int objectTransferAttempts = 5)
        {
            var blobs = Blob.Convert(jobResponse).ToList();
            var allItems = partialObjects
                .Concat(PartialObjectRangeUtilities.ObjectPartsForFullObjects(fullObjects, blobs))
                .ToList();
            return new PartialReadJob(
                client,
                jobResponse,
                jobResponse.BucketName,
                jobResponse.JobId,
                helperStrategy,
                PartialObjectRangeUtilities.RangesForRequests(blobs, allItems),
                allItems,
                allItems.Select(po => ContextRange.Create(Range.ByLength(0L, po.Range.Length), po)),
                objectTransferAttempts
            );
        }

        private PartialReadJob(
            IDs3Client client,
            MasterObjectList jobResponse,
            string bucketName,
            Guid jobId,
            IHelperStrategy<Ds3PartialObject> helperStrategy,
            ILookup<Blob, Range> rangesForRequests,
            IEnumerable<Ds3PartialObject> allItems,
            IEnumerable<ContextRange<Ds3PartialObject>> itemsToTrack,
            int objectTransferAttempts = 5)
                : base(
                        client,
                        jobResponse,
                        bucketName,
                        jobId,
                        helperStrategy,
                        new PartialDataTransferStrategyDecorator(new PartialReadTransferStrategy(), objectTransferAttempts),
                        rangesForRequests,
                        new RequestToObjectRangeTranslator(rangesForRequests).ComposedWith(new ObjectToPartRangeTranslator(allItems)),
                        itemsToTrack,
                        objectTransferAttempts
                      )
        {
            this.AllItems = allItems;
        }

        public IEnumerable<Ds3PartialObject> AllItems { get; private set; }
    }
}