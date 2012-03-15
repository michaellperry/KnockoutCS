using System;
using UpdateControls.Fields;

namespace KnockoutCS
{
    public class Observable<T> : Independent<T>
    {
        public Observable()
        {
        }

        public Observable(T value)
            : base(value)
        {
        }

        public Observable(string name, T value)
            : base(name, value)
        {
        }

        public Observable(Type containerType, string name)
            : base(containerType, name)
        {
        }

        public Observable(Type containerType, string name, T value)
            : base(containerType, name, value)
        {
        }
    }
}
