
using System.Collections.Generic;
using System;

namespace ASHFramework.Mono.Diagnostics
{
    public class MAExceptionCatcher
    {
        private Exception _exception = null;

        private List<Type> _continuableExceptions = new List<Type>();

        public void CatchException(Exception ex)
        {
            _exception = ex;
        }

        public Exception GetException()
        {
            return _exception;
        }

        public T GetException<T>() where T : Exception
        {
            return _exception as T;
        }

        public bool TryGetException<T>(out T exception) where T : Exception
        {
            exception = _exception as T;
            if (exception == null)
                return false;

            return true;
        }

        public bool HasException()
        {
            return (_exception != null);
        }

        public bool IsExceptionTypeOf<T>() where T : Exception
        {
            return _exception is T;
        }

        public void AddContinuableException<T>() where T : Exception
        {
            _continuableExceptions.Add(typeof(T));
        }

        public bool IsContinuableException()
        {
            return _continuableExceptions.Contains(_exception.GetType());
        }
    }
}
