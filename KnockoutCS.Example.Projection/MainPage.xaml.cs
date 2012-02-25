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
                    select new PersonSummary(person)
                ),
                SelectedPerson = KO.Computed(
                    () => selection.SelectedPerson == null
                        ? null
                        : new PersonSummary(selection.SelectedPerson),
                    value => selection.SelectedPerson = value == null
                        ? null
                        : value.Person
                ),
                NewPerson = KO.Command(() =>
                {
                    Person newPerson = KO.NewObservable<Person>();
                    newPerson.FirstName = "Michael";
                    newPerson.LastName = "Perry";
                    phoneBook.People.Add(newPerson);
                }),
                PersonDetail = KO.Computed(() => selection.SelectedPerson)
            });
        }
    }
}
