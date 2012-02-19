using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using KnockoutCS.Library.Impl;

namespace KnockoutCS.Library
{
    public static class KO
    {
        public static dynamic Observable<T>(T model)
        {
            return new Observable<T>(model);
        }

        public static object ApplyBindings(object viewModelTemplate)
        {
            if (viewModelTemplate == null)
                return null;
            IObjectInstance root = (IObjectInstance)typeof(ObjectInstance<>)
                .MakeGenericType(viewModelTemplate.GetType())
                .GetConstructors()
                .Single()
                .Invoke(new object[] { viewModelTemplate, Deployment.Current.Dispatcher });
            return root;
        }
    }
}
