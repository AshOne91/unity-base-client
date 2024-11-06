using System;

using ASHFramework.Mono.Pattern;

namespace ASHFramework.Mono.Diagnostics
{
    public class MAExceptionAssert : MAAssertInterface
    {
        public void Assert(bool condition, string message)
        {
            if (condition == false)
                throw new Exception("[UT] " + message);
        }
    }
}
