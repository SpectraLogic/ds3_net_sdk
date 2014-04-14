using System.Linq;
using System.Management.Automation;
using Ds3Client.Configuration;
using Ds3Client.Api;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommon.Get, DS3Nouns.Bucket)]
    public class GetBucketCommand : BaseApiCommand
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string BucketName { get; set; }

        protected override void ProcessRecord()
        {
            if (BucketName != null)
            {
                throw new ApiException(Resources.BucketNameNotImplementedException);
            }

            using (var response = CreateClient().GetService(new Ds3.Models.GetServiceRequest()))
            {
                foreach (var bucket in response.Buckets)
                {
                    WriteObject(bucket);
                }
            }
        }
    }
}
