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

namespace Ds3.Helpers
{
    /// <summary>
    /// Wraps another object part tracker in locks. Each method call should be
    /// atomic, and we don't need locks across calls.
    /// </summary>
    internal class ConcurrentObjectPartTracker : IObjectPartTracker
    {
        private readonly IObjectPartTracker _innerTracker;
        private readonly object _lock = new object();

        public ConcurrentObjectPartTracker(IObjectPartTracker innerTracker)
        {
            this._innerTracker = innerTracker;
        }

        public event Action<long> DataTransferred
        {
            add
            {
                lock (this._lock)
                {
                    this._innerTracker.DataTransferred += value;
                }
            }
            remove
            {
                lock (this._lock)
                {
                    this._innerTracker.DataTransferred -= value;
                }
            }
        }

        public event Action Completed
        {
            add
            {
                lock (this._lock)
                {
                    this._innerTracker.Completed += value;
                }
            }
            remove
            {
                lock (this._lock)
                {
                    this._innerTracker.Completed -= value;
                }
            }
        }

        public void CompletePart(ObjectPart partToRemove)
        {
            lock (this._lock)
            {
                this._innerTracker.CompletePart(partToRemove);
            }
        }


        public bool ContainsPart(ObjectPart part)
        {
            lock (this._lock)
            {
                return this._innerTracker.ContainsPart(part);
            }
        }
    }
}
