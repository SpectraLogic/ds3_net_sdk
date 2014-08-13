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

using NUnit.Framework;

using Ds3.Helpers;
using Part = Ds3.Helpers.ObjectPartPlanner.ObjectPart;

namespace TestDs3
{
    [TestFixture]
    public class TestObjectPartPlanner
    {
        private static readonly PartComparer _partComparer = new PartComparer();

        [Test]
        public void TestPlanParts()
        {
            CheckPlanParts(0L, 1024L, new Part(0L, 1024L, 1));
            CheckPlanParts(512L, 1024L, new Part(512L, 1024L, 1));
            CheckPlanParts(0L, 1025L, new Part(0L, 1024L, 1), new Part(1024L, 1L, 2));
            CheckPlanParts(512L, 1025L, new Part(512L, 1024L, 1), new Part(1536L, 1L, 2));
            CheckPlanParts(1536L, 1025L, new Part(1536L, 1024L, 1), new Part(2560L, 1L, 2));
            CheckPlanParts(512L, 3072L, new Part(512L, 1024L, 1), new Part(1536L, 1024L, 2), new Part(2560L, 1024L, 3));
        }

        private static void CheckPlanParts(long offset, long length, params Part[] expected)
        {
            var objectParts = ObjectPartPlanner.PlanParts(1024L, offset, length).ToList();
            CollectionAssert.AreEqual(objectParts, expected.ToList(), _partComparer);
        }

        private class PartComparer : IComparer, IComparer<Part>
        {
            public int Compare(Part x, Part y)
            {
                return x.Length != y.Length
                    ? Math.Sign(x.Length - y.Length)
                    : ( x.Offset != y.Offset
                        ? Math.Sign(x.Offset - y.Offset)
                        : ( x.PartNumber != y.PartNumber
                            ? Math.Sign(x.PartNumber - y.PartNumber)
                            : 0
                        )
                    );
            }

            public int Compare(object x, object y)
            {
                if (x is Part && y is Part)
                {
                    return this.Compare((Part)x, (Part)y);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}
