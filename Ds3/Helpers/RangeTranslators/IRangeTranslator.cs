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

using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers.RangeTranslators
{
    public interface IRangeTranslator<TFromContext, TToContext>
        where TFromContext : IComparable<TFromContext>
        where TToContext : IComparable<TToContext>
    {
        IEnumerable<ContextRange<TToContext>> Translate(ContextRange<TFromContext> contextRange);
    }

    internal static class RangeTranslatorExtensions
    {
        public static IRangeTranslator<TFromContext, TToContext> ComposedWith<TFromContext, TBetweenContext, TToContext>(
            this IRangeTranslator<TFromContext, TBetweenContext> first,
            IRangeTranslator<TBetweenContext, TToContext> second)
                where TFromContext : IComparable<TFromContext>
                where TBetweenContext : IComparable<TBetweenContext>
                where TToContext : IComparable<TToContext>
        {
            return new ComposedRangeTranslator<TFromContext, TBetweenContext, TToContext>(first, second);
        }

        private class ComposedRangeTranslator<TFromContext, TBetweenContext, TToContext>
            : IRangeTranslator<TFromContext, TToContext>
                where TFromContext : IComparable<TFromContext>
                where TBetweenContext : IComparable<TBetweenContext>
                where TToContext : IComparable<TToContext>
        {
            private IRangeTranslator<TFromContext, TBetweenContext> _first;
            private IRangeTranslator<TBetweenContext, TToContext> _second;

            public ComposedRangeTranslator(
                IRangeTranslator<TFromContext, TBetweenContext> first,
                IRangeTranslator<TBetweenContext, TToContext> second)
            {
                this._first = first;
                this._second = second;
            }

            public IEnumerable<ContextRange<TToContext>> Translate(ContextRange<TFromContext> contextRange)
            {
                return this._first.Translate(contextRange).SelectMany(this._second.Translate);
            }
        }
    }
}
