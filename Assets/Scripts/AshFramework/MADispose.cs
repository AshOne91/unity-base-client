using System;

namespace ASHFramework.Mono
{
    public abstract class MADispose : MAObject, IDisposable
    {
        private bool _disposed = true;
        public bool _Disposed
        {
            get { return _disposed; }
        }

        protected abstract void Construct();
        protected abstract void Destruct();

        protected MADispose() 
        {
            _disposed = false;
            Construct();
        }
        ~MADispose()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing) 
        { 
            if (_disposed == false)
            {
                _disposed = true;
                if(disposing == true)
                {
                    Destruct();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // 가비지 컬렉션 큐에서 제거(이미 호출했음)
            GC.SuppressFinalize(this);
        }
    }

    public abstract class MADisposeEX : MAObject, IDisposable
    {
        private bool _disposed = true;

        protected MADisposeEX()
        {
            _disposed = false;
        }

        ~MADisposeEX()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed == false) 
            { 
                _disposed = true;
                if (disposing == true)
                {
                    OnDispose();
                }
            }
        }

        protected abstract void OnDispose();

        public bool IsDisposed()
        {
            return _disposed;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
