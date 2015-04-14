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

namespace TestDs3.Lang
{
    /// <summary>
    /// Provides a means of easily implementing ICompare instances
    /// for values of T based on a series of delegates over T.
    ///
    /// This was implemented because comparers can be tedious and
    /// error prone to write, and we need them a lot in test code.
    /// This was originally created when a specific model class
    /// with lots and lots of fields came about.
    /// </summary>
    internal static class CompareChain
    {
        public static CompareChain<T> Of<T>(T x, T y)
        {
            return new CompareChain<T>(x, y);
        }
    }

    internal struct CompareChain<T>
    {
        private readonly T _x;
        private readonly T _y;

        public int Result { get; private set; }

        public CompareChain(T x, T y)
            : this(x, y, 0)
        {
        }

        private CompareChain(T x, T y, int result)
            : this()
        {
            this._x = x;
            this._y = y;
            this.Result = result;
        }

        public CompareChain<T> Value<TValue>(Func<T, TValue> valueOf)
        {
            return this.Value(valueOf, Comparer<TValue>.Default);
        }

        public CompareChain<T> Value<TValue>(Func<T, TValue> valueOf, IComparer<TValue> comparer)
        {
            return new CompareChain<T>(
                this._x,
                this._y,
                this.Result == 0
                    ? comparer.Compare(valueOf(this._x), valueOf(this._y))
                    : this.Result
            );
        }
    }
}
