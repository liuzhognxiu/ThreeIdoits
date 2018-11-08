using System;
using System.Diagnostics;

namespace MogoEngine.Utils
{
    public class Singleton<T> where T : IInit, IDispose, new()
    {
        protected static T _instance = default(T);
        private static Object _objLock = new System.Object();

        protected Singleton()
        {
            Debug.Assert(_instance == null);
        }

        public static T Instance
        {
            get
            {
                if (null != _instance)
                {
                    return _instance;
                }

                lock (_objLock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                        _instance.Init();        
                    }
                }

                return _instance;
            }
        }
    }

    public interface IInit
    {
        void Init();
    }

    public interface IDispose
    {
        void Dispose();
    }
}
