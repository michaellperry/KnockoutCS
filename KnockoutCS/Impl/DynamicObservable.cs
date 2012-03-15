using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace KnockoutCS.Impl
{
    public class DynamicObservable<T> : DynamicObject
    {
        private Dictionary<string, DynamicObservableProperty<T>> _propertyByName = new Dictionary<string, DynamicObservableProperty<T>>();
        
        public DynamicObservable(T model)
        {
            foreach (PropertyInfo property in model.GetType().GetProperties())
                _propertyByName.Add(property.Name, new DynamicObservableProperty<T>(model, property));
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
            DynamicObservableProperty<T> property;
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
            DynamicObservableProperty<T> property;
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
