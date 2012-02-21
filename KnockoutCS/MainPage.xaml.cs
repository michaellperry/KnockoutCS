using System.Windows;
using System.Windows.Controls;
using KnockoutCS.Library;

namespace KnockoutCS
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            dynamic model = KO.Observable(new Model());
            DataContext = KO.ApplyBindings(new
            {
                FirstName = KO.Computed(() => model.FirstName, value => model.FirstName = value),
                LastName = KO.Computed(() => model.LastName, value => model.LastName = value),
                FullName = KO.Computed(() => model.FirstName + " " + model.LastName)
            });
        }
    }
}
