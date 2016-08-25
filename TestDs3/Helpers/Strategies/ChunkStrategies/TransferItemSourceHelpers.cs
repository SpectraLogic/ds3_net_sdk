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

using Ds3;
using Ds3.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Ds3.Helpers.Strategies.ChunkStrategies;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.Strategies.ChunkStrategies
{
    [TestFixture]
    public class TransferItemSourceHelpers
    {
        [Test]
        public void TestTransferItemComparer()
        {
            var comparer = new TransferItemComparer();

            var client1 = new Mock<IDs3Client>(MockBehavior.Strict).Object;
            var client2 = new Mock<IDs3Client>(MockBehavior.Strict).Object;

            var blob1 = new Blob(Range.ByLength(10, 11), "foo");
            var blob2 = new Blob(Range.ByLength(10, 11), "bar");

            var instance1 = new TransferItem(client1, blob1);

            Assert.AreEqual(0, comparer.Compare(instance1, instance1));
            Assert.AreEqual(0, comparer.Compare(new TransferItem(client1, blob1), new TransferItem(client1, blob1)));
            Assert.AreNotEqual(0, comparer.Compare(new TransferItem(client1, blob1), new TransferItem(client2, blob1)));
            Assert.AreEqual(1, comparer.Compare(instance1, new TransferItem(client1, blob2)));
        }

        internal class TransferItemComparer : IComparer<TransferItem>, IComparer
        {
            public int Compare(object x, object y)
            {
                var xti = x as TransferItem;
                var yti = y as TransferItem;
                if (xti != null && yti != null)
                {
                    return this.Compare(xti, yti);
                }
                throw new ArgumentException();
            }

            public int Compare(TransferItem x, TransferItem y)
            {
                return Math.Sign(
                    2 * Math.Sign(x.Client.GetHashCode() - y.Client.GetHashCode())
                    + x.Blob.CompareTo(y.Blob)
                );
            }
        }
    }
}