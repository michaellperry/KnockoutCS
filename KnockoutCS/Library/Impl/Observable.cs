using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace KnockoutCS.Library.Impl
{
    public class Observable<T> : DynamicObject
    {
        private Dictionary<string, ObservableProperty<T>> _propertyByName = new Dictionary<string, ObservableProperty<T>>();
        
        public Observable(T model)
        {
            foreach (PropertyInfo property in model.GetType().GetProperties())
                _propertyByName.Add(property.Name, new ObservableProperty<T>(model, property));
        }

        public object Get(string propertyName)
        {
            return _propertyByName[propertyName].Get();
        }

        public void Set(string propertyName, object value)
        {
            _propertyByName[propertyName].Set(value);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            ObservableProperty<T> property;
            if (_propertyByName.TryGetValue(binder.Name, out property))
            {
                result = property.Get();
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            ObservableProperty<T> property;
            if (_propertyByName.TryGetValue(binder.Name, out property))
            {
                property.Set(value);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
