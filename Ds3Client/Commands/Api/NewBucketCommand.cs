using System.Management.Automation;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommon.New, DS3Nouns.Bucket)]
    public class NewBucketCommand : BaseApiCommand
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true)]
        public string BucketName { get; set; }

        protected override void ProcessRecord()
        {
            using (CreateClient().PutBucket(new Ds3.Models.PutBucketRequest(BucketName)))
            {
            }
        }
    }
}
