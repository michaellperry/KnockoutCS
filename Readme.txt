Here is the API that I'm currently thinking of:

    private void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        Model model = new Model();
        dynamic viewModel = KO.Observable(model);
        KO.Bind(firstName, TextBox.TextProperty, viewModel.FirstName);
        KO.Bind(lastName, TextBox.TextProperty, viewModel.LastName);
        KO.Bind(fullName, TextBox.TextProperty, viewModel.FirstName + " " + viewModel.LastName);
    }

Where the model is simply:

    public class Model
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
