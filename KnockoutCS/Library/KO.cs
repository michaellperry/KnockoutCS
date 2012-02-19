using System;
using System.Windows;
using System.Windows.Data;

namespace KnockoutCS.Library1
{
    public static class KO
    {
        public static Binding Observable(Func<string> property)
        {
            throw new NotImplementedException();
        }

        public static Binding Computed(Func<string> computation)
        {
            throw new NotImplementedException();
        }

        public static dynamic Observable<T>(T model)
        {
            throw new NotImplementedException();
        }

        public static void Bind(DependencyObject control, DependencyProperty targetProperty, dynamic sourceProperty)
        {
            throw new NotImplementedException();
        }

        public static object ApplyBindings(object viewModelTemplate)
        {
            throw new NotImplementedException();
        }
    }
}
