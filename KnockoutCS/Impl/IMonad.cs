using System;

namespace KnockoutCS.Impl
{
    public interface IMonad
    {
        object GetObject();
        void SetObject(object value);
    }
}
