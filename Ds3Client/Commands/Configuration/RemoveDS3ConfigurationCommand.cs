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
