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

using System.Collections.Generic;
using System.Linq;

namespace Ds3Client.Configuration
{
    class Session : ISession
    {
        private readonly IDictionary<string, Configuration> _configurations = new Dictionary<string, Configuration>();

        public void Import(Configuration configuration)
        {
            if (_configurations.ContainsKey(configuration.Name))
            {
                throw new ConfigurationException(Resources.SessionExistsException, configuration.Name);
            }
            _configurations.Add(configuration.Name, configuration);
        }

        public IEnumerable<Configuration> Get()
        {
            return _configurations.Values;
        }

        public Configuration Get(string name)
        {
            return _configurations[name];
        }

        public Configuration GetSelected()
        {
            if (_configurations.Count == 0)
            {
                throw new ConfigurationException(Resources.NoSelectedSessionException);
            }
            var session = _configurations.Values.SingleOrDefault(config => config.IsSelected);
            if (session == null)
            {
                throw new ConfigurationException(Resources.NoSelectedSessionException);
            }
            return session;
        }

        public void Remove(string name)
        {
            _configurations.Remove(name);
        }

        public void Select(string name)
        {
            if (!_configurations.ContainsKey(name))
            {
                throw new ConfigurationException(Resources.NoSuchSessionException, name);
            }
            foreach (var config in _configurations.Values)
            {
                config.IsSelected = config.Name == name;
            }
        }
    }
}
