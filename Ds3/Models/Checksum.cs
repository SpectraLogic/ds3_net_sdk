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

        public abstract void Match(Action none, Action compute, Action<byte[]> value);
        public abstract T Match<T>(Func<T> none, Func<T> compute, Func<byte[], T> value);

        private Checksum()
        {
            // Prevent non-internal implementations.
        }

        private class ValueChecksum : Checksum
        {
            private readonly byte[] _hash;

            public ValueChecksum(byte[] hash)
            {
                if (hash.Length != 16)
                {
                    throw new ArgumentException(string.Format("Parameter must be a 16-byte MD5 hash value."));
                }
                this._hash = hash;
            }

            public override void Match(Action none, Action compute, Action<byte[]> value)
            {
                value(_hash);
            }

            public override T Match<T>(Func<T> none, Func<T> compute, Func<byte[], T> value)
            {
                return value(_hash);
            }
        }

        private class ComputeChecksum : Checksum
        {
            public override void Match(Action none, Action compute, Action<byte[]> value)
            {
                compute();
            }

            public override T Match<T>(Func<T> none, Func<T> compute, Func<byte[], T> value)
            {
                return compute();
            }
        }

        private class NoneChecksum : Checksum
        {
            public override void Match(Action none, Action compute, Action<byte[]> value)
            {
                none();
            }

            public override T Match<T>(Func<T> none, Func<T> compute, Func<byte[], T> value)
            {
                return none();
            }
        }
    }
}
