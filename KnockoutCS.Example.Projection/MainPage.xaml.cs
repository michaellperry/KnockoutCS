using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;

namespace KnockoutCS.Example.Projection
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            PhoneBook phoneBook = KO.NewObservable<PhoneBook>();
            DataContext = KO.ApplyBindings(phoneBook, new
            {
                People = KO.Computed(() =>
                    from person in phoneBook.People
                    select PersonSummary.FromPerson(person)
                ),
                NewPerson = KO.Command(() =>
                {
                    Person newPerson = KO.NewObservable<Person>();
                    newPerson.FirstName = "New";
                    newPerson.LastName = String.Format("Person{0}", phoneBook.People.Count+1);
                    phoneBook.People.Add(newPerson);
                })
            });
        }
    }
}
