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
