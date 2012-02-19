using System;

namespace KnockoutCS.Library.Impl
{
    public class Monad
    {
        private Func<object> _getter;
        private Action<object> _setter;

        public Monad(Func<object> getter, Action<object> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        public object Get()
        {
            return _getter();
        }

        public void Set(object value)
        {
            if (_setter != null)
                _setter(value);
        }

        public static Monad operator +(Monad l, Monad r)
        {
            return new Monad(
                delegate
                {
                    return (string)(l.Get()) + (string)(r.Get());
                },
                null);
        }

        public static Monad operator +(Monad l, string c)
        {
            return new Monad(
                delegate
                {
                    return (string)(l.Get()) + c;
                },
                null);
        }
    }
}
