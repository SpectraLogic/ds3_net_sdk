using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Linq;
using Config = Ds3Client.Configuration.Configuration;

namespace TestDs3Client.Configuration
{
    [TestFixture]
    public class TestNewRemoveGetCommands
    {
        private readonly Config _expectedConfig1 = new Config
        {
            Name = "TestConfig",
            Endpoint = new Uri("http://the.endpoint/the/path"),
            AccessKey = "TXlBS2V5",
            SecretKey = "TXlTS2V5",
            IsSelected = false
        };

        private readonly Config _expectedConfig2 = new Config
        {
            Name = "AnotherTestConfig",
            Endpoint = new Uri("http://another.endpoint/another/path"),
            AccessKey = "TXlPdGhlckFLZXk=",
            SecretKey = "TXlPdGhlclNLZXk=",
            IsSelected = true
        };

        [Test]
        public void TestNewGetRemoveGet()
        {
            using (var runspace = Helpers.SetUpRunspace())
            {
                // Add one.
                Helpers.RunAndTestNew(runspace, _expectedConfig1, ps => ps.AddParameter("DontPersist"));
                Helpers.RunAndTestGet(runspace, new Collection<Config> { _expectedConfig1 });

                // Add another.
                Helpers.RunAndTestNew(runspace, _expectedConfig2, ps => ps.AddParameter("DontPersist").AddParameter("MakeSelected"));
                Helpers.RunAndTestGet(runspace, new Collection<Config> { _expectedConfig1, _expectedConfig2 });

                // Remove one.
                Helpers.RunAndTestRemove(runspace, _expectedConfig1);

                // Check the results.
                Helpers.RunAndTestGet(runspace, new Collection<Config> { _expectedConfig2 });

                runspace.Close();
            }
        }
    }
}
