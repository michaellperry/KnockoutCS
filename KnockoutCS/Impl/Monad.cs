using System;

namespace KnockoutCS.Impl
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
    }
}
