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

using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Ds3Client.Configuration
{
    class SessionSingleton
    {
        private static IDictionary<Guid, ISession> _sessions = new Dictionary<Guid, ISession>();

        public static ISession Current
        {
            get
            {
                // Get info about the current runspace.
                // The current runspace is what the user understands to be the current session.
                var runspace = Runspace.DefaultRunspace;
                var instanceId = runspace.InstanceId;

                // Return the session for the current runspace if it exists or create a new one.
                if (_sessions.ContainsKey(instanceId))
                {
                    return _sessions[instanceId];
                }
                else
                {
                    // Create and remember the runspace.
                    var session = new Session();
                    _sessions[instanceId] = session;

                    // When the runspace goes away let's forget the session so we don't leak memory.
                    runspace.StateChanged += (object sender, RunspaceStateEventArgs e) =>
                    {
                        if (e.RunspaceStateInfo.State != RunspaceState.Opened)
                        {
                            _sessions.Remove(instanceId);
                        }
                    };

                    return session;
                }
            }
        }
    }
}
