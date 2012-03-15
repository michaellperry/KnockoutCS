using System.Reflection;
using UpdateControls;

namespace KnockoutCS.Impl
{
    public class DynamicObservableProperty<T>
    {
        private readonly T _model;
        private readonly PropertyInfo _property;

        private Independent _independent = new Independent();

        public DynamicObservableProperty(T model, PropertyInfo property)
        {
            _model = model;
            _property = property;
        }

        public object Get()
        {
            _independent.OnGet();
            return _property.GetValue(_model, null);
        }

        public void Set(object value)
        {
            _independent.OnSet();
            _property.SetValue(_model, value, null);
        }
    }
}
