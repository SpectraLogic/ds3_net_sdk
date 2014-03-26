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

        protected override void ProcessRecord()
        {
            if (KeyPrefix != null)
                throw new NotImplementedException(Resources.KeyPrefixNotImplementedException);

            using (var response = CreateClient().GetBucket(new Ds3.Models.GetBucketRequest(BucketName)))
            {
                foreach (var ds3Object in response.Objects)
                    WriteObject(ds3Object);
            }
        }
    }
}
