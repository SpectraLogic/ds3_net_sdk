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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using Config = Ds3Client.Configuration.Configuration;

namespace TestDs3Client.Configuration
{
    class Helpers
    {
        public static readonly Config SuccessConfig = new Config
        {
            Name = "SuccessConfig",
            Endpoint = new Uri("http://the.end.point/the/path"),
            AccessKey = "c3BlY3RyYQ==",
            SecretKey = "bG9naWM=",
            IsSelected = false
        };
        public static readonly Config AdditionalConfig = new Config
        {
            Name = "AdditionalConfig",
            Endpoint = new Uri("http://the.other.end.point/the/path"),
            AccessKey = "bG9naWNhbA==",
            SecretKey = "c3BlY3RydW1z",
            IsSelected = false
        };
        public static readonly Config FailConfig1 = new Config
        {
            Name = "FailConfig1",
            Endpoint = new Uri("http://the.end.point/the/path"),
            AccessKey = "c3BlY3RyYWFh",
            SecretKey = "bG9naWM=",
            IsSelected = false
        };

        /// <summary>
        /// Creates a PowerShell runspace and imports Ds3Client.dll as a module within the runspace.
        /// 
        /// Need to using and call .Close() on the runspace.
        /// </summary>
        /// <returns></returns>
        public static Runspace SetUpRunspace()
        {
            var runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            // Build the module directory.
            var modulesDir = Path.Combine(TestContext.CurrentContext.TestDirectory, "Modules");
            var moduleDir = Path.Combine(modulesDir, "Ds3Client");
            if (Directory.Exists(moduleDir))
                Directory.Delete(moduleDir, true);
            Directory.CreateDirectory(moduleDir);

            // Set the PowerShell paths for where to look for modules.
            var previousPath = Environment.GetEnvironmentVariable("PSModulePath");
            if (!previousPath.Contains("Ds3Client"))
                Environment.SetEnvironmentVariable("PSModulePath", previousPath + ";" + modulesDir);

            // Copy the dll.
            var dllPath = Uri.UnescapeDataString(new UriBuilder(Assembly.Load("Ds3Client").CodeBase).Path);
            File.Copy(dllPath, Path.Combine(moduleDir, Path.GetFileName(dllPath)));

            // Create the new module manifest.
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps
                    .AddCommand("New-ModuleManifest")
                    .AddArgument(Path.Combine(moduleDir, "Ds3Client.psd1"))
                    .AddParameters(new Dictionary<string, object> {
                        { "ModuleToProcess", Path.Combine(moduleDir, "Ds3Client.dll") }, 
                        { "Author", "Spectra Logic" }, 
                        { "CompanyName", "Spectra Logic" }, 
                        { "Copyright", 2014 }, 
                        { "NestedModules", new object[0] }, 
                        { "Description", "" }, 
                        { "TypesToProcess", new object[0] }, 
                        { "FormatsToProcess", new object[0] }, 
                        { "RequiredAssemblies", new object[0] }, 
                        { "FileList", new object[0] }
                    });
                ps.Invoke();
            }

            // Import the module.
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps
                    .AddCommand("Import-Module")
                    .AddArgument("Ds3Client");
                ps.Invoke();
            }

            return runspace;
        }

        public static void AssertConfigEqual(Config expected, Config actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Endpoint, actual.Endpoint);
            Assert.AreEqual(expected.AccessKey, actual.AccessKey);
            Assert.AreEqual(expected.SecretKey, actual.SecretKey);
            Assert.AreEqual(expected.IsSelected, actual.IsSelected);
        }

        public static Config CopyConfig(Config config)
        {
            return new Config
            {
                Name = config.Name,
                Endpoint = config.Endpoint,
                AccessKey = config.AccessKey,
                SecretKey = config.SecretKey,
                IsSelected = config.IsSelected
            };
        }

        public static void RunAndTestNew(Runspace runspace, Config expected, Action<PowerShell> flagSets)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps
                    .AddCommand("New-Ds3Configuration")
                    .AddParameters(new Dictionary<string, object> {
                        { "Name", expected.Name },
                        { "Endpoint", expected.Endpoint },
                        { "AccessKey", expected.AccessKey },
                        { "SecretKey", expected.SecretKey }
                    });
                flagSets(ps);
                var result = ps.Invoke<Config>();

                Assert.AreEqual(1, result.Count);
                AssertConfigEqual(expected, result[0]);
            }
        }

        public static void RunAndTestGet(Runspace runspace, Collection<Config> expected)
        {
            RunAndTestGet(runspace, expected, ps => { });
        }

        public static void RunAndTestGet(Runspace runspace, Collection<Config> expected, Action<PowerShell> flagSet)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddCommand("Get-Ds3Configuration");
                flagSet(ps);
                var actual = ps.Invoke<Config>();

                Assert.AreEqual(expected.Count, actual.Count);
                for (var i = 0; i < expected.Count; i++)
                    AssertConfigEqual(expected[i], actual[i]);
            }
        }

        public static void RunAndTestImport(Runspace runspace, string filename, Config expected, Action<PowerShell> flagSets)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps
                    .AddCommand("Import-DS3Configuration")
                    .AddArgument(Path.Combine(TestContext.CurrentContext.TestDirectory, string.Format(@"Configuration\TestData\{0}.xml", filename)));
                flagSets(ps);
                var result = ps.Invoke<Config>();

                Assert.AreEqual(1, result.Count);
                AssertConfigEqual(expected, result[0]);
            }
        }

        public static void RunAndTestSelect(Runspace runspace, Config expected)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps
                    .AddCommand("Select-DS3Configuration")
                    .AddArgument(expected.Name);
                var result = ps.Invoke<Config>();

                Assert.AreEqual(1, result.Count);
                AssertConfigEqual(expected, result[0]);
            }
        }

        public static void RunAndTestRemove(Runspace runspace, Config expectedConfig)
        {
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps
                    .AddCommand("Remove-Ds3Configuration")
                    .AddParameter("Name", expectedConfig.Name);
                var result = ps.Invoke<Config>();
                Assert.AreEqual(1, result.Count);
                AssertConfigEqual(expectedConfig, result[0]);
            }
        }
    }
}
