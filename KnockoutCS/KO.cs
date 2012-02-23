using System;
using System.Linq;
using System.Windows;
using KnockoutCS.Impl;

namespace KnockoutCS
{
    public static class KO
    {
        public static TModel NewObservable<TModel>()
        {
            ObservableTypeBuilder typeBuilder = new ObservableTypeBuilder(typeof(TModel));
            return (TModel)Activator.CreateInstance(typeBuilder.CreateType());
        }

        public static dynamic Observable<TModel>(TModel model)
        {
            return new Observable<TModel>(model);
        }

        public static Monad Computed(Func<object> computation)
        {
            return new Monad(computation, null);
        }

        public static Monad Computed(Func<object> computation, Action<object> inverse)
        {
            return new Monad(computation, inverse);
        }

        public static object ApplyBindings<TModel>(TModel model, object viewModel)
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
