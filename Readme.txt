Create a view model and apply bindings:

  private void MainPage_Loaded(object sender, RoutedEventArgs e)
  {
    dynamic model = KO.Observable(new Model());
    DataContext = KO.ApplyBindings<Model>(model, new
    {
      FullName = KO.Computed(() => model.FirstName + " " + model.LastName)
    });
  }

Where the model is simply:

  public class Model
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }

This will data bind to all properties of the model plus the computed properties
of the view model, as in:

  <TextBox Text="{Binding FirstName, Mode=TwoWay}"/>
  <TextBox Text="{Binding LastName, Mode=TwoWay}"/>
  <TextBlock Text="{Binding FullName}"/>
