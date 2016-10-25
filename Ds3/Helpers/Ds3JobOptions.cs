/*
 * ******************************************************************************
 *   Copyright 2014-2016 Spectra Logic Corporation. All Rights Reserved.
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
using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.Helpers
{
    public abstract class Ds3JobOptions
    {
        private Priority? _priority;
        public bool? Aggregating { get; set; }
        public string Name { get; set; }

        public Priority? Priority
        {
            get { return _priority; }
            set
            {
                if (!value.HasValue) return;
                if (IsForbiddenPriority(value.Value))
                {
                    throw new Ds3ForbiddenPriorityException(string.Format(Resources.ForbiddenPriorityException,
                        value));
                }
                _priority = value;
            }
        }

        private static bool IsForbiddenPriority(Priority priority)
        {
            var forbiddenPriorities = new HashSet<Priority> {Models.Priority.BACKGROUND, Models.Priority.CRITICAL};
            return forbiddenPriorities.Contains(priority);
        }
    }
}