using System;
using System.Windows;
using System.Windows.Input;
using KnockoutCS.Impl;
using UpdateControls.XAML;

namespace KnockoutCS
{
    public static class KO
    {
        public static TModel NewObservable<TModel>()
        {
            return (TModel)Activator.CreateInstance(ObservableTypeBuilder.CreateType(typeof(TModel)));
        }

        public static dynamic DynamicObservable<TModel>(TModel model)
        {
            return new DynamicObservable<TModel>(model);
        }

        public static Monad<T> Computed<T>(Func<T> computation)
        {
            return new Monad<T>(computation, null);
        }

        public static Monad<T> Computed<T>(Func<T> computation, Action<T> inverse)
        {
            return new Monad<T>(computation, inverse);
        }

        public static ICommand Command(Action execute)
        {
            return MakeCommand.Do(execute);
        }

        public static ICommand Command(Action execute, Func<bool> condition)
        {
            return MakeCommand.When(condition).Do(execute);
        }

        public static object ApplyBindings<TModel, TViewModel>(TModel model, TViewModel viewModel)
        {
            return new ObjectInstance<TModel, TViewModel>(model, viewModel, Deployment.Current.Dispatcher);
        }
    }
}
