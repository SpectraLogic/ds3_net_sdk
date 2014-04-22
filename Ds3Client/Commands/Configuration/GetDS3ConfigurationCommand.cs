/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */

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
                {
                    WriteObject(config);
                }
            }
        }
    }
}
