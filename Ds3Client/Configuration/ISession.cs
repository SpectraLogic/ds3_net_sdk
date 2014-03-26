using System.Collections.Generic;

namespace Ds3Client.Configuration
{
    interface ISession
    {
        void Import(Configuration configuration);
        IEnumerable<Configuration> Get();
        Configuration Get(string name);
        Configuration GetSelected();
        void Remove(string name);
        void Select(string name);
    }
}
