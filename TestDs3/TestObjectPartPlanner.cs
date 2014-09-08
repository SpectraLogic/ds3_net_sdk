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

namespace TestDs3
{
    [TestFixture]
    public class TestObjectPartPlanner
    {
        [Test]
        public void TestPlanParts()
        {
            CheckPlanParts(0L, 1024L, new ObjectPart(0L, 1024L));
            CheckPlanParts(512L, 1024L, new ObjectPart(512L, 1024L));
            CheckPlanParts(0L, 1025L, new ObjectPart(0L, 1024L), new ObjectPart(1024L, 1L));
            CheckPlanParts(512L, 1025L, new ObjectPart(512L, 1024L), new ObjectPart(1536L, 1L));
            CheckPlanParts(1536L, 1025L, new ObjectPart(1536L, 1024L), new ObjectPart(2560L, 1L));
            CheckPlanParts(512L, 3072L, new ObjectPart(512L, 1024L), new ObjectPart(1536L, 1024L), new ObjectPart(2560L, 1024L));
        }

        private static void CheckPlanParts(long offset, long length, params ObjectPart[] expected)
        {
            var objectParts = ObjectPartPlanner.PlanParts(1024L, offset, length).ToList();
            CollectionAssert.AreEqual(objectParts, expected.ToList(), Comparer<ObjectPart>.Default);
        }
    }
}
