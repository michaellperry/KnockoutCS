using System;

namespace KnockoutCS.Impl
{
    public class Monad<T> : IMonad
    {
        private Func<T> _getter;
        private Action<T> _setter;

        public Monad(Func<T> getter, Action<T> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        public T Get()
        {
            return _getter();
        }

        public void Set(T value)
        {
            if (_setter != null)
                _setter(value);
        }

        public object GetObject()
        {
            return Get();
        }

        public void SetObject(object value)
        {
            Set((T)value);
        }
    }
}
