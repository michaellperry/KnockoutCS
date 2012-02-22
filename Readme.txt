Create a view model and apply bindings:

  private void MainPage_Loaded(object sender, RoutedEventArgs e)
  {
    Model model = KO.NewObservable<Model>();
    DataContext = KO.ApplyBindings(model, new
    {
        FullName = KO.Computed(() => model.FirstName + " " + model.LastName)
    });
  }

Where the model is simply:

  public class Model
  {
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
  }

This will data bind to all properties of the model plus the computed properties
of the view model, as in:

  <TextBox Text="{Binding FirstName, Mode=TwoWay}"/>
  <TextBox Text="{Binding LastName, Mode=TwoWay}"/>
  <TextBlock Text="{Binding FullName}"/>
