using System;
using System.Collections.Generic;

using Ds3.Models;

namespace Ds3Client.Commands.Api
{
    internal static class GetAllObjectsHelper
    {
        private const int _defaultMaxKeys = 1000;

        public static IEnumerable<Ds3Object> GetAllObjects(Ds3.Ds3Client client, string bucketName, string keyPrefix)
        {
            return GetAllObjects(client, bucketName, keyPrefix, int.MaxValue);
        }

        public static IEnumerable<Ds3Object> GetAllObjects(Ds3.Ds3Client client, string bucketName, string keyPrefix, int maxKeys)
        {
            var remainingKeys = maxKeys;
            var isTruncated = false;
            string marker = null;
            do
            {
                var request = new Ds3.Calls.GetBucketRequest(bucketName)
                {
                    Marker = marker,
                    MaxKeys = Math.Min(remainingKeys, _defaultMaxKeys),
                    Prefix = keyPrefix
                };
                using (var response = client.GetBucket(request))
                {
                    isTruncated = response.IsTruncated;
                    marker = response.NextMarker;
                    remainingKeys -= response.Objects.Count;
                    foreach (var ds3Object in response.Objects)
                    {
                        yield return ds3Object;
                    }
                }
            } while (isTruncated && remainingKeys > 0);
        }
    }
}
