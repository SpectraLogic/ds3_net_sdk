using System;

namespace Ds3.Runtime
{
    public static class RuntimeUtils
    {
        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }
    }
}
