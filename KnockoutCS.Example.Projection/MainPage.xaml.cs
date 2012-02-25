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
using System.Linq;

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
            PhoneBookSelection selection = KO.NewObservable<PhoneBookSelection>();
            DataContext = KO.ApplyBindings(phoneBook, new
            {
                People = KO.Computed(() =>
                    from person in phoneBook.People
                    select PersonSummary.FromPerson(person)
                ),
                SelectedPerson = KO.Computed(
                    () => PersonSummary.FromPerson(selection.SelectedPerson),
                    value => selection.SelectedPerson = PersonSummary.ToPerson(value)
                ),
                NewPerson = KO.Command(() =>
                {
                    Person newPerson = KO.NewObservable<Person>();
                    newPerson.FirstName = "New";
                    newPerson.LastName = "Person";
                    phoneBook.People.Add(newPerson);
                    selection.SelectedPerson = newPerson;
                }),
                DeletePerson = KO.Command(() =>
                {
                    phoneBook.People.Remove(selection.SelectedPerson);
                    selection.SelectedPerson = null;
                },
                    () => selection.SelectedPerson != null
                ),
                PersonDetail = KO.Computed(() => selection.SelectedPerson)
            });
        }
    }
}
