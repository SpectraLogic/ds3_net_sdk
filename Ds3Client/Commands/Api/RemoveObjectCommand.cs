using System.Management.Automation;
using System.IO;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommon.Remove, DS3Nouns.Object)]
    public class RemoveObjectCommand : BaseApiCommand
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true)]
        public string BucketName { get; set; }

        [Alias(new string[] { "Name" })]
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Key { get; set; }

        [Parameter]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            var confirmString = string.Format(Resources.ObjectDescription, Key, BucketName);
            if (Force.IsPresent || (ShouldProcess(confirmString) && ShouldContinue(Resources.RemoveObjectConfirmationMessage, confirmString)))
            {
                using (CreateClient().DeleteObject(new Ds3.Models.DeleteObjectRequest(BucketName, Key)))
                {
                }
            }
        }
    }
}
