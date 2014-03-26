using NUnit.Framework;
using System.Reflection;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System;
using System.Collections.Generic;
using Ds3Client.Configuration;
using Config  = Ds3Client.Configuration.Configuration;
using System.Collections.ObjectModel;

namespace TestDs3Client.Configuration
{
    [TestFixture]
    public class TestImportGetCommands
    {
        [Test]
        public void TestBasicImport()
        {
            using (var runspace = Helpers.SetUpRunspace())
            {
                Helpers.RunAndTestImport(runspace, "SuccessConfig", Helpers.SuccessConfig, ps => { });
                Helpers.RunAndTestGet(runspace, new Collection<Config> { Helpers.SuccessConfig });
                runspace.Close();
            }
        }

        [Test]
        public void TestDoubleImport()
        {
            using (var runspace = Helpers.SetUpRunspace())
            {
                var selectedSuccess = Helpers.CopyConfig(Helpers.SuccessConfig);
                selectedSuccess.IsSelected = true;
                Helpers.RunAndTestImport(runspace, "SuccessConfig", selectedSuccess, ps => ps.AddParameter("MakeSelected"));

                var renamedAdditional = Helpers.CopyConfig(Helpers.AdditionalConfig);
                renamedAdditional.Name = "CoolerConfig";
                Helpers.RunAndTestImport(runspace, "AdditionalConfig", renamedAdditional, ps => ps.AddParameter("Name", "CoolerConfig"));

                Helpers.RunAndTestGet(runspace, new Collection<Config> { selectedSuccess, renamedAdditional });

                runspace.Close();
            }
        }
    }
}
