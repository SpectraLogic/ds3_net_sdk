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
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ds3.Models
{
    public abstract class Checksum
    {
        private static Checksum _none = new NoneChecksum();
        private static Checksum _compute = new ComputeChecksum();

        public static Checksum None
        {
            get { return _none; }
        }

        public static Checksum Compute
        {
            get { return _compute; }
        }

        public static Checksum Value(byte[] hash)
        {
            return new ValueChecksum(hash);
        }

        public T Match<T>(Func<T> none, Func<T> compute, Func<byte[], T> value)
        {
            T result = default(T);
            Match(
                delegate { result = none(); },
                delegate { result = compute(); },
                hash => { result = value(hash); }
            );
            return result;
        }

        public void Match(Action none, Action compute, Action<byte[]> value)
        {
            if (this == _none)
            {
                none();
            }
            else if (this == _compute)
            {
                compute();
            }
            else
            {
                var it = this as ValueChecksum;
                Debug.Assert(it != null);
                value(it.Hash);
            }
        }

        private Checksum()
        {
            // Prevent non-internal implementations.
        }

        private class ValueChecksum : Checksum
        {
            public byte[] Hash { get; private set; }

            public ValueChecksum(byte[] hash)
            {
                if (hash.Length != 16)
                {
                    throw new ArgumentException(string.Format("Parameter must be a 16-byte MD5 hash value."));
                }
                this.Hash = hash;
            }
        }

        private class ComputeChecksum : Checksum
        {
        }

        private class NoneChecksum : Checksum
        {
        }
    }
}
