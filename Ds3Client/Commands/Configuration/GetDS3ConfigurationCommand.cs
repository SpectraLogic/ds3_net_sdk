using Ds3Client.Configuration;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Ds3Client.Commands.Configuration
{
    [Cmdlet(VerbsCommon.Get, DS3Nouns.Configuration)]
    public class GetDS3ConfigurationCommand : PSCmdlet
    {
        [Parameter(Position = 0)]
        public string Name { get; set; }

        [Parameter]
        public SwitchParameter Selected { get; set; }

        protected override void EndProcessing()
        {
            var session = SessionSingleton.Current;

            // Get the selected config.
            if (Selected)
            {
                WriteObject(session.GetSelected());
            }
            // Get the config by name.
            else if (Name != null)
            {
                WriteObject(session.Get(Name));
            }
            // Get all configs.
            else
            {
                foreach (var config in session.Get())
                    WriteObject(config);
            }
        }
    }
}
