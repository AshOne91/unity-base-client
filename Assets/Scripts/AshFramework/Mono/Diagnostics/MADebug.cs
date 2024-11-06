using ASHFramework.Mono.Pattern;
using ASHFramework.Mono.IO
using ASHFramework.Mono.IO.Logger;
using System;

namespace ASHFramework.Mono.Diagnostics
{
    public sealed class MADebug
    {
        static private MAAssertInterface _assert = new MAExceptionAssert();
        static private MALogger _logger = new MALogger("./Log/MALogger.txt");

        #region Methods
        static public void Assert(bool condition, string message)
        {
            _assert.Assert(condition, message);
        }

        static public void Log(object message)
        {
            Console.WriteLine((message == null) ? "[MT] null" : "[MT] " + message.ToString());
        }

        static public void LogWarning(object message)
        {
            Console.WriteLine((message == null) ? "[MT] null" : "[MT] " + message.ToString());
        }

        static public void LogError(object message)
        {
            string error = (message == null) ? "[MT] null" : string.Format("[MT] {0}", message.ToString());
            Console.WriteLine(error);
            Write(error);
        }

        static public void Write(string text)
        {
            _logger.Write(text);
        }

        //////////////////////////////////////////////////////////////////////////////////////////

        static public void Assert(bool condition, string format, params object[] messages)
        {
            Assert(condition, string.Format(format, messages));
        }

        static public void Log(string format, params object[] messages)
        {
            Log((object)string.Format(format, messages));
        }

        static public void LogWarning(string format, params object[] messages)
        {
            LogWarning((object)string.Format(format, messages));
        }

        static public void LogError(string format, params object[] messages)
        {
            LogError((object)string.Format(format, messages));
        }

        static public void Write(string format, params object[] messages)
        {
            _logger.Write(format, messages);
        }
    #endregion
    }
}
