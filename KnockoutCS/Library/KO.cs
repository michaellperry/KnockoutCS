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

        public static object Computed(Func<object> computation)
        {
            return new Monad(computation, null);
        }

        public static object Computed(Func<object> computation, Action<object> inverse)
        {
            return new Monad(computation, inverse);
        }

        public static object ApplyBindings<TModel>(dynamic model, object viewModel)
        {
            if (viewModel == null)
                return null;
            IObjectInstance root = (IObjectInstance)typeof(ObjectInstance<,>)
                .MakeGenericType(typeof(TModel), viewModel.GetType())
                .GetConstructors()
                .Single()
                .Invoke(new object[] { model, viewModel, Deployment.Current.Dispatcher });
            return root;
        }
    }
}
