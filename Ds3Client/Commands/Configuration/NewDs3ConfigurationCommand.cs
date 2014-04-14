using Ds3Client.Configuration;
using System;
using System.IO;
using System.Management.Automation;
using IOPath = System.IO.Path;
using Config = Ds3Client.Configuration.Configuration;

namespace Ds3Client.Commands.Configuration
{
    [Cmdlet(VerbsCommon.New, DS3Nouns.Configuration)]
    public class NewDs3ConfigurationCommand : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        [Parameter(Mandatory = true)]
        public Uri Endpoint { get; set; }

        [Parameter(Mandatory = true)]
        public string AccessKey { get; set; }

        [Parameter(Mandatory = true)]
        public string SecretKey { get; set; }

        [Parameter]
        public Uri Proxy { get; set; }

        [Parameter]
        public string Path { get; set; }

        [Parameter]
        public SwitchParameter MakeSelected { get; set; }

        [Parameter]
        public SwitchParameter DontPersist { get; set; }

        protected override void EndProcessing()
        {
            // Build and validate the config based on parameters.
            var config = new Config
            {
                Name = Name,
                Endpoint = Endpoint,
                Proxy = Proxy,
                AccessKey = AccessKey,
                SecretKey = SecretKey,
                IsSelected = false
            };
            ConfigurationParser.ValidateConfiguration(config);

            // Import and optionally select the config.
            var session = SessionSingleton.Current;
            session.Import(config);
            if (MakeSelected)
            {
                session.Select(config.Name);
            }

            // Save the config unless the user told us not to.
            if (!DontPersist)
            {
                // Write the file.
                var path = PreparePath();
                using (var outFile = File.OpenWrite(path))
                {
                    ConfigurationParser.Unparse(config, outFile);
                }

                // Warn the user that we've written a file since it may not be obvious otherwise.
                WriteWarning(string.Format(Resources.WroteConfigFileWarning, path));
            }

            // Send the new config to the output.
            WriteObject(config);
        }

        private string PreparePath()
        {
            // Use the path from the user if they specified it.
            if (Path != null)
            {
                return Path;
            }

            // Create the directory if it doesn't exist.
            var defaultDirectory = IOPath.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"SpectraLogic\BlackPearl");
            if (!Directory.Exists(defaultDirectory))
            {
                Directory.CreateDirectory(defaultDirectory);
            }

            // Build the path.
            return IOPath.Combine(defaultDirectory, Name + ".xml");
        }
    }
}
