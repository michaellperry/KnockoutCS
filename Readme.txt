Here is the API that I'm currently thinking of:

    private void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        dynamic model = KO.Observable(new Model());
        DataContext = KO.ApplyBindings(new
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            FullName = model.FirstName + " " + model.LastName
        });
    }

Where the model is simply:

    public class Model
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
