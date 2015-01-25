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

using Ds3.Models;

namespace Ds3.Helpers.RangeTranslators
{
    internal class RangeMapping<T>
    {
        public Range From { get; private set; }
        public long To { get; private set; }
        public T Context { get; private set; }

        public RangeMapping(Range from, long to, T context)
        {
            this.From = from;
            this.To = to;
            this.Context = context;
        }

        public override string ToString()
        {
            return string.Format("RangeMapping{{from={0},to={1},context='{2}'}}", this.From, this.To, this.Context);
        }
    }
}
