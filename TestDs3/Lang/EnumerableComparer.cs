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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestDs3.Lang
{
    internal class EnumerableComparer<T> : IComparer, IComparer<IEnumerable<T>>
    {
        private readonly IComparer<T> _innerComparer;

        public EnumerableComparer()
            : this(Comparer<T>.Default)
        {
        }

        public EnumerableComparer(IComparer<T> innerComparer)
        {
            this._innerComparer = innerComparer;
        }

        public int Compare(object x, object y)
        {
            return this.Compare(x as IEnumerable<T>, y as IEnumerable<T>);
        }

        public int Compare(IEnumerable<T> x, IEnumerable<T> y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException();
            }

            var xList = x.ToList();
            var yList = y.ToList();

            var countCompareResult = xList.Count.CompareTo(yList.Count);
            if (countCompareResult != 0)
            {
                return countCompareResult;
            }

            for (int i = 0; i < xList.Count; i++)
            {
                var itemCompareResult = this._innerComparer.Compare(xList[i], yList[i]);
                if (itemCompareResult != 0)
                {
                    return itemCompareResult;
                }
            }
            return 0;
        }
    }
}
