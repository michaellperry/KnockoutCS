using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using KnockoutCS.Library1;
using UpdateControls.XAML;

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
            Model model = new Model();
            dynamic viewModel = KO.Observable(model);
            KO.Bind(firstName, TextBox.TextProperty, viewModel.FirstName);
            KO.Bind(lastName, TextBox.TextProperty, viewModel.LastName);
            KO.Bind(fullName, TextBox.TextProperty, viewModel.FirstName + " " + viewModel.LastName);
        }
    }
}
