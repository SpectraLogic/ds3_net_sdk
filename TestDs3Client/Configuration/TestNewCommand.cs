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

using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Config = Ds3Client.Configuration.Configuration;

namespace TestDs3Client.Configuration
{
    [TestFixture]
    public class TestNewCommand
    {
        private const string _successConfigDefaultPath = @"SpectraLogic\BlackPearl\SuccessConfig.xml";
        private const string _successConfigResourceName = "TestDs3Client.Configuration.TestData.SuccessConfig.xml";

        [Test]
        public void TestNewWritesFile()
        {
            using (var runspace = Helpers.SetUpRunspace())
            {
                // Delete the default file so we can ensure the result is due to our test.
                var defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _successConfigDefaultPath);
                if (File.Exists(defaultPath))
                    File.Delete(defaultPath);

                // Run the New-DS3Configuration command.
                Helpers.RunAndTestNew(runspace, Helpers.SuccessConfig, ps => { });

                // Check the result.
                Assert.That(File.Exists(defaultPath));
                Assert.AreEqual(ReadResource(_successConfigResourceName), File.ReadAllText(defaultPath));
            }
        }

        [Test]
        public void TestNewDontWriteFile()
        {
            using (var runspace = Helpers.SetUpRunspace())
            {
                // Delete the default file so we can ensure the result is due to our test.
                var writtenFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _successConfigDefaultPath);
                if (File.Exists(writtenFile))
                    File.Delete(writtenFile);

                // Run the New-DS3Configuration command.
                Helpers.RunAndTestNew(runspace, Helpers.SuccessConfig, ps => ps.AddParameter("DontPersist"));

                // Check the result.
                Assert.That(!File.Exists(writtenFile));
            }
        }

        [Test]
        public void TestNewWriteToPath()
        {
            using (var runspace = Helpers.SetUpRunspace())
            {
                // Make sure the location we're writing to doesn't already have a file.
                var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "OtherLocation.xml");
                if (File.Exists(path))
                    File.Delete(path);

                // Make sure the default path also doesn't exist so we can verify that it's not written to.
                var defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _successConfigDefaultPath);
                if (File.Exists(defaultPath))
                    File.Delete(defaultPath);

                // Run the New-DS3Configuration command with the Path argument.
                Helpers.RunAndTestNew(runspace, Helpers.SuccessConfig, ps => ps.AddParameter("Path", path));

                // Check the result.
                Assert.That(!File.Exists(defaultPath));
                Assert.That(File.Exists(path));
                Assert.AreEqual(ReadResource(_successConfigResourceName), File.ReadAllText(path));
            }
        }

        [Test]
        public void TestNewMakeSelected()
        {
            using (var runspace = Helpers.SetUpRunspace())
            {
                // Make a success config that's selected.
                var selectedSuccessConfig = Helpers.CopyConfig(Helpers.SuccessConfig);
                selectedSuccessConfig.IsSelected = true;

                // Add a few configurations.
                Helpers.RunAndTestNew(runspace, selectedSuccessConfig, ps => ps.AddParameter("MakeSelected"));
                Helpers.RunAndTestNew(runspace, Helpers.AdditionalConfig, ps => { });

                // Check the selected item result.
                Helpers.RunAndTestGet(runspace, new Collection<Config> { selectedSuccessConfig, Helpers.AdditionalConfig });
            }
        }

        private static string ReadResource(string resourceName)
        {
            using (var xmlFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(xmlFile))
                return reader.ReadToEnd();
        }
    }
}
