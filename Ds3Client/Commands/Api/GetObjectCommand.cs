using System;
using System.Linq;
using System.Management.Automation;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommon.Get, DS3Nouns.Object)]
    public class GetObjectCommand : BaseApiCommand
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true)]
        public string BucketName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string KeyPrefix { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int? MaxKeys { get; set; }

        protected override void ProcessRecord()
        {
            var ds3Objects = MaxKeys.HasValue
                ? GetAllObjectsHelper.GetAllObjects(CreateClient(), BucketName, KeyPrefix, MaxKeys.Value)
                : GetAllObjectsHelper.GetAllObjects(CreateClient(), BucketName, KeyPrefix);
            foreach (var ds3Object in ds3Objects)
            {
                WriteObject(ds3Object);
            }
        }
    }
}
