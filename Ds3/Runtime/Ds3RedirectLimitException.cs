using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ds3.Runtime
{
    class Ds3RedirectLimitException : Ds3RequestException
    {
        public Ds3RedirectLimitException(string message)
            : base(message)
        {
        }
    }
}
