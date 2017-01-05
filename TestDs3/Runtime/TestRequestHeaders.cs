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

using Ds3.Runtime;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TestDs3.Runtime
{
    [TestFixture]
    public class TestRequestHeaders
    {
        private static IRequestHeaders GetTestRequestHeaders()
        {
            var rh = new RequestHeaders();
            rh.Add("Key One", "Val+One");
            rh.Add("Key Two", "Val+Two");
            rh.Add("Key Three", "Val+Three");
            return rh;
        }

        [Test]
        public void TestAddNull()
        {
            var rh = new RequestHeaders();
            Assert.Catch(typeof(System.ArgumentNullException), () => rh.Add(null, null));
            Assert.Catch(typeof(System.ArgumentNullException), () => rh.Add(null, "Value"));
        }

        [Test]
        public void TestAddWithSpaces()
        {
            var expected = new Dictionary<string, string>
            {
                { "Key%20With%20Spaces", "Val%20With%20Spaces" }
            };

            var rh = new RequestHeaders();
            rh.Add("Key With Spaces", "Val With Spaces");
            Assert.AreEqual(rh.Headers.Count, 1);
            Assert.AreEqual(rh.Headers, expected);
        }

        [Test]
        public void TestRemoveNull()
        {
            var rh = new RequestHeaders();
            Assert.Catch(typeof(System.ArgumentNullException), () => rh.Remove(null));
        }

        [Test]
        public void TestRemove()
        {
            var rh = GetTestRequestHeaders();

            Assert.AreEqual(rh.Headers.Count, 3);
            Assert.IsFalse(rh.Remove("Key%20One"));

            Assert.IsTrue(rh.Remove("Key One"));
            Assert.AreEqual(rh.Headers.Count, 2);
            Assert.IsFalse(rh.Headers.ContainsKey("Key One"));

            Assert.IsTrue(rh.Remove("Key Two"));
            Assert.AreEqual(rh.Headers.Count, 1);
            Assert.IsFalse(rh.Headers.ContainsKey("Key Two"));

            Assert.IsTrue(rh.Remove("Key Three"));
            Assert.AreEqual(rh.Headers.Count, 0);
        }

        [Test]
        public void TestKeys()
        {
            var rh = GetTestRequestHeaders();
            var result = rh.Keys();

            Assert.AreEqual(result.Count, 3);
            Assert.Contains("Key One", result);
            Assert.Contains("Key Two", result);
            Assert.Contains("Key Three", result);
        }

        [Test]
        public void TestHeaders()
        {
            var expected = new Dictionary<string, string>
            {
                { "Key%20One", "Val%2BOne" },
                { "Key%20Two", "Val%2BTwo" },
                { "Key%20Three", "Val%2BThree" }
            };

            var rh = GetTestRequestHeaders();
            Assert.AreEqual(rh.Headers, expected);
        }
    }
}
