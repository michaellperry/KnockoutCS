using System.Windows;
using System.Windows.Controls;
using UpdateControls.XAML;
using System;

namespace KnockoutCS.Example.Collection
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Parent parent = KO.NewObservable<Parent>();
            Parent parent = new Parent__Observable();
            DataContext = KO.ApplyBindings(parent, new
            {
                NewChild = MakeCommand.
                    Do(() =>
                    {
                        Child child = KO.NewObservable<Child>();
                        child.Name = String.Format("Child {0}", parent.Children.Count + 1);
                        parent.Children.Add(child);
                    })
            });
        }
    }
}
