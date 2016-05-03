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

// This code is auto-generated, do not modify

using System;

namespace Ds3.Models
{
    public abstract class ChecksumType
    {
        private static ChecksumType _none = new NoneChecksumType();
        private static ChecksumType _compute = new ComputeChecksumType();

        public enum Type {
            CRC_32,
            CRC_32C,
            MD5,
            SHA_256,
            SHA_512,
            NONE
        }

        /// <summary>
        /// Do not provide a ChecksumType header on PUT.
        /// </summary>
        public static ChecksumType None
        {
            get { return _none; }
        }

        /// <summary>
        /// Calculate the ChecksumType automatically. This requires a seekable streem.
        /// </summary>
        public static ChecksumType Compute
        {
            get { return _compute; }
        }

        /// <summary>
        /// Provide a binary ChecksumType value directly, if the client
        /// application knows the ChecksumType of a payload beforehand.
        /// </summary>
        /// <param name="hash">The ChecksumType bytes</param>
        /// <returns>The ChecksumType "value" instance</returns>
        public static ChecksumType Value(byte[] hash)
        {
            return new ValueChecksumType(hash);
        }

        /// <summary>
        /// Calls none, compute, or value, depending on which type this actually is.
        /// </summary>
        /// <param name="none">The function to call if the value is "none".</param>
        /// <param name="compute">The function to call if the value is "compute".</param>
        /// <param name="value">The function to call if the value is "value" with a ChecksumType payload.</param>
        public abstract void Match(Action none, Action compute, Action<byte[]> value);

        /// <summary>
        /// Calls none, compute, or value, depending on which type this actually is.
        /// </summary>
        /// <param name="none">The function to call if the value is "none".</param>
        /// <param name="compute">The function to call if the value is "compute".</param>
        /// <param name="value">The function to call if the value is "value" with a ChecksumType payload.</param>
        /// <returns>What either none, computer, or value return.</returns>
        public abstract T Match<T>(Func<T> none, Func<T> compute, Func<byte[], T> value);

        private ChecksumType()
        {
            // Prevent non-internal implementations.
        }

        private class ValueChecksumType : ChecksumType
        {
            private readonly byte[] _hash;

            public ValueChecksumType(byte[] hash)
            {
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

        private class ComputeChecksumType : ChecksumType
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

        private class NoneChecksumType : ChecksumType
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