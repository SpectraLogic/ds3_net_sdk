/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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

using System.Collections.Generic;
using Ds3.Helpers;
using Ds3.Models;
using NUnit.Framework;

namespace TestDs3.Helpers
{
    [TestFixture]
    public class TestFileSystemHelpers
    {
        private static readonly string Path = System.IO.Path.GetTempPath();
        private static readonly IEnumerable<Ds3Object> ObjectsToRead = new List<Ds3Object> {new Ds3Object("test", 0)};

        private static readonly object[] CheckError =
        {
            new object[] {"", ObjectsToRead},
            new object[] {null, ObjectsToRead},
            new object[] {Path, null}
        };

        private static readonly object[] CheckPathNotExists =
        {
            new object[] {Path + "NotExists"},
            new object[] {@"\\UNC_path_test"}
        };

        [Test]
        public void TestCheck()
        {
            var result = FileSystemHelpers.Check(Path, ObjectsToRead);

            Assert.AreEqual(DriveStatus.Ok, result.Status);
        }

        [Test]
        [TestCaseSource(nameof(CheckError))]
        public void TestCheckError(string path, IEnumerable<Ds3Object> objectsToRead)
        {
            var result = FileSystemHelpers.Check(path, objectsToRead);

            Assert.AreEqual(DriveStatus.Error, result.Status);
        }

        [Test]
        [TestCaseSource(nameof(CheckPathNotExists))]
        public void TestCheckPathNotExists(string path)
        {
            var result = FileSystemHelpers.Check(path, ObjectsToRead);

            Assert.AreEqual(DriveStatus.DirectoryNotExists, result.Status);
        }
    }
}