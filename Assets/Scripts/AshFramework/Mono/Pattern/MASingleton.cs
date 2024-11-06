using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ASHFramework.Mono.Pattern
{
    //�̱����� ������ �ۼ��������
    public abstract class MASingleton<T> :IDisposable where T : class
    {
        static private bool _disposed = true;
        static public bool _Disposed
        {
            get { return _disposed; }
        }

        static private volatile T _instance = null;
        static public T _Instance
        {
            get { return _instance; }
        }
        
        protected MASingleton() 
        {
            _instance = (T)(object)this;
            _disposed = false;

            Construct();
        }

        ~MASingleton() 
        {
            Dispose(false);
        }

        protected abstract void Construct();
        protected abstract void Destruct();

        private void Dispose(bool disposing)
        {
            if (_disposed == false)
            {
                _disposed = true;

                if (disposing == true)
                {
                    Destruct();
                }
            }

            _instance = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    //������ �̱��濡 ���
    public class MAStaticSingleton<T> where T : class
    {
        private class SingletonCreator : MAObject
        {
            static SingletonCreator() { }
            //����� �����ڸ� ȣ���ϱ� ����
            internal static readonly T _instance = typeof(T).InvokeMember(typeof(T).Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null) as T;
        }

        static public T _Instance
        {
            get { return SingletonCreator._instance; }
        }
    }
}
