using Ds3Client.Configuration;
using System.Management.Automation;

namespace Ds3Client.Commands.Configuration
{
    [Cmdlet(VerbsCommon.Remove, DS3Nouns.Configuration)]
    public class RemoveDS3ConfigurationCommand : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Name { get; set; }

        protected override void EndProcessing()
        {
            var session = SessionSingleton.Current;

            // Get the config so we can return it.
            var config = session.Get(Name);

            // Remove the config.
            session.Remove(Name);

            // Write the removed config to the output.
            WriteObject(config);
        }
    }
}
