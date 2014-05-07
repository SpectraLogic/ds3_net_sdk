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

using System.Management.Automation;
using Ds3Client.Configuration;
using Config = Ds3Client.Configuration.Configuration;
using Ds3;

namespace Ds3Client.Commands.Api
{
    public abstract class BaseApiCommand : PSCmdlet
    {
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Config Configuration { get; set; }

        protected IDs3Client CreateClient()
        {
            var config = Configuration ?? SessionSingleton.Current.GetSelected();
            var builder = new Ds3.Ds3Builder(config.Endpoint.ToString(), new Ds3.Credentials(config.AccessKey, config.SecretKey));
            if (config.Proxy != null)
            {
                builder.WithProxy(config.Proxy);
            }
            return builder.Build();
        }
    }
}
