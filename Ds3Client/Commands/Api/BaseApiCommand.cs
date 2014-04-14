using System.Management.Automation;
using Ds3Client.Configuration;
using Config = Ds3Client.Configuration.Configuration;

namespace Ds3Client.Commands.Api
{
    public abstract class BaseApiCommand : PSCmdlet
    {
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Config Configuration { get; set; }

        protected Ds3.Ds3Client CreateClient()
        {
            var config = Configuration ?? SessionSingleton.Current.GetSelected();
            var builder = new Ds3.Ds3Builder(config.Endpoint.ToString(), new Ds3.Credentials(config.AccessKey, config.SecretKey));
            if (config.Proxy != null)
            {
                builder.WithProxy(config.Proxy);
            }
            return builder.Build();
        }
    }
}
