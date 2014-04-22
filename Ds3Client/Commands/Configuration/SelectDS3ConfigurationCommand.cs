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
