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
            return (TModel)Activator.CreateInstance(ObservableTypeBuilder.CreateType(typeof(TModel)));
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

        public static object ApplyBindings<TModel, TViewModel>(TModel model, TViewModel viewModel)
        {
            return new ObjectInstance<TModel, TViewModel>(model, viewModel, Deployment.Current.Dispatcher);
        }
    }
}
