using System.Management.Automation;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommon.Remove, DS3Nouns.Bucket, SupportsShouldProcess = true)]
    public class RemoveBucketCommand : BaseApiCommand
    {
        [Alias(new string[] { "Name" })]
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true)]
        public string BucketName { get; set; }

        [Parameter]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            var confirmString = string.Format(Resources.BucketDescription, BucketName);
            if (Force.IsPresent || (ShouldProcess(confirmString) && ShouldContinue(Resources.RemoveBucketConfirmationMessage, confirmString)))
            {
                using (CreateClient().DeleteBucket(new Ds3.Models.DeleteBucketRequest(BucketName)))
                {
                }
            }
        }
    }
}
