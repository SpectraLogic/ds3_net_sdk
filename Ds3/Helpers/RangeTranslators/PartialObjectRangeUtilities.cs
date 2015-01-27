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

using Ds3.Lang;
using Ds3.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers.RangeTranslators
{
    internal static class PartialObjectRangeUtilities
    {
        public static IEnumerable<Ds3PartialObject> ObjectPartsForFullObjects(
            IEnumerable<string> fullObjects,
            IEnumerable<Blob> blobs)
        {
            var fullObjectsSet = new HashSet<string>(fullObjects);
            return
                from blob in blobs
                where fullObjectsSet.Contains(blob.Context)
                group blob.Range.End by blob.Context into grp
                select new Ds3PartialObject(Range.ByPosition(0L, grp.Max()), grp.Key);
        }

        public static ILookup<Blob, Range> RangesForRequests(
            IEnumerable<Blob> blobs,
            IEnumerable<Ds3PartialObject> partialObjects)
        {
            return (
                from blob in blobs
                group blob.Range by blob.Context into blobsOfObj
                join partsOfObj in
                    from po in partialObjects group po.Range by po.Context
                    on blobsOfObj.Key equals partsOfObj.Key
                from range in RangesForObjectRequests(blobsOfObj, partsOfObj)
                group range.Range by new Blob(range.Context, blobsOfObj.Key)
            ).ToLookup();
        }

        internal static IEnumerable<ContextRange<Range>> RangesForObjectRequests(
            IEnumerable<Range> blobs,
            IEnumerable<Range> parts)
        {
            using (var blobIter = blobs.GetEnumerator())
            using (var partIter = parts.GetEnumerator())
            {
                var hasNextblob = blobIter.MoveNext();
                var hasNextPart = partIter.MoveNext();
                while (hasNextblob && hasNextPart)
                {
                    var blob = blobIter.Current;
                    var part = partIter.Current;
                    if (blob.Overlaps(part))
                    {
                        yield return ContextRange.Create(blob.Intersect(part), blob);
                    }
                    if (blob.End <= part.End)
                    {
                        hasNextblob = blobIter.MoveNext();
                    }
                    if (blob.End >= part.End)
                    {
                        hasNextPart = partIter.MoveNext();
                    }
                }
            }
        }
    }
}
