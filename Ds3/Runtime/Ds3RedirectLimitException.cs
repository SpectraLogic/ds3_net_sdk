using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ds3.Runtime
{
    public class Ds3RedirectLimitException : Ds3RequestException
    {
        internal Ds3RedirectLimitException(string message)
            : base(message)
        {
        }
    }
}
