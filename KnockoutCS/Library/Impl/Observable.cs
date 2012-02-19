using System.Collections.Generic;
using System.Dynamic;
using UpdateControls;
using System.Reflection;

namespace KnockoutCS.Library.Impl
{
    public class Observable<T> : DynamicObject
    {
        private T _model;
        private Dictionary<string, Independent> _independentByPropertyName = new Dictionary<string, Independent>();
        
        public Observable(T model)
        {
            _model = model;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            Independent independent = GetIndependentByPropertyName(binder.Name);
            PropertyInfo property = GetPropertyByPropertyName(binder.Name);

            result = new Monad(
                delegate
                {
                    independent.OnGet();
                    return property.GetValue(_model, null);
                },
                delegate(object value)
                {
                    independent.OnSet();
                    property.SetValue(_model, value, null);
                }
            );
            return true;
        }

        private Independent GetIndependentByPropertyName(string propertyName)
        {
            Independent independent;
            if (!_independentByPropertyName.TryGetValue(propertyName, out independent))
            {
                independent = new Independent();
                _independentByPropertyName.Add(propertyName, independent);
            }
            return independent;
        }

        private PropertyInfo GetPropertyByPropertyName(string propertyName)
        {
            PropertyInfo property = _model.GetType().GetProperty(propertyName);
            return property;
        }
    }
}
