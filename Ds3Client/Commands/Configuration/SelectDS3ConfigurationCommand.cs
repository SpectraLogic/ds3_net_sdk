using Ds3Client.Configuration;
using System.Management.Automation;

namespace Ds3Client.Commands.Configuration
{
    [Cmdlet(VerbsCommon.Select, DS3Nouns.Configuration)]
    public class SelectDS3ConfigurationCommand : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Name { get; set; }

        protected override void EndProcessing()
        {
            var session = SessionSingleton.Current;

            // Select the session by name.
            session.Select(Name);

            // Output the selected session.
            WriteObject(session.Get(Name));
        }
    }
}
