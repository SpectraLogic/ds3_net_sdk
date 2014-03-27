using System;
using System.Linq;
using System.Management.Automation;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommon.Get, DS3Nouns.Object)]
    public class GetObjectCommand : BaseApiCommand
    {
        private const int _defaultMaxKeys = 1000;

        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true)]
        public string BucketName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string KeyPrefix { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int? MaxKeys { get; set; }

        protected override void ProcessRecord()
        {
            var client = CreateClient();
            var remainingKeys = MaxKeys.HasValue ? MaxKeys.Value : int.MaxValue;
            var isTruncated = false;
            string marker = null;
            do
            {
                var request = new Ds3.Models.GetBucketRequest(BucketName) {
                    Marker = marker,
                    MaxKeys = Math.Min(remainingKeys, _defaultMaxKeys),
                    Prefix = KeyPrefix
                };
                using (var response = client.GetBucket(request))
                {
                    isTruncated = response.IsTruncated;
                    marker = response.NextMarker;
                    remainingKeys -= response.Objects.Count;
                    foreach (var ds3Object in response.Objects)
                        WriteObject(ds3Object);
                }
            } while (isTruncated && remainingKeys > 0);
        }
    }
}
