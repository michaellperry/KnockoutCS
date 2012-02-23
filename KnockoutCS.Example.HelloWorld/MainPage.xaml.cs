using System.Windows;
using System.Windows.Controls;
using KnockoutCS;

namespace KnockoutCS.Example.HelloWorld
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Model model = KO.NewObservable<Model>();
            DataContext = KO.ApplyBindings(model, new
            {
                FullName = KO.Computed(() => model.FirstName + " " + model.LastName)
            });
            // Equally valid:
            // DataContext = KO.ApplyBindings(model, new ViewModel(model));
        }
    }
}
