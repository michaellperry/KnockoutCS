using System;
using System.Windows;
using System.Windows.Input;
using KnockoutCS.Impl;
using UpdateControls.XAML;
using System.Linq;

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
            // Make sure that we can access the assembly's property info.
            try
            {
                var propertyInfo = typeof(TViewModel).GetProperties().FirstOrDefault(p => p.CanRead);
                if (propertyInfo != null)
                    propertyInfo.GetValue(viewModel, null);
            }
            catch (MethodAccessException ex)
            {
                throw new InvalidOperationException(@"Open Properties\AssemblyInfo.cs and add the line [assembly: InternalsVisibleTo(""KnockoutCS"")].");
            }
            return new ObjectInstance<TModel, TViewModel>(model, viewModel, Deployment.Current.Dispatcher);
        }
    }
}
