using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace KnockoutCS.Example.Selection
{
    public class PhoneBookViewModel
    {
        private readonly PhoneBook _phoneBook;
        private readonly PhoneBookSelection _selection;

        public PhoneBookViewModel(PhoneBook phoneBook, PhoneBookSelection selection)
        {
            _phoneBook = phoneBook;
            _selection = selection;
        }

        public IEnumerable<PersonSummary> People
        {
            get
            {
                return
                    from person in _phoneBook.People
                    select new PersonSummary(person);
            }
        }

        public PersonSummary SelectedPerson
        {
            get { return PersonSummary.FromPerson(_selection.SelectedPerson); }
            set { _selection.SelectedPerson = PersonSummary.ToPerson(value); }
        }

        public ICommand NewPerson
        {
            get
            {
                return KO.Command(() =>
                {
                    Person newPerson = KO.NewObservable<Person>();
                    newPerson.FirstName = "New";
                    newPerson.LastName = "Person";
                    _phoneBook.People.Add(newPerson);
                    _selection.SelectedPerson = newPerson;
                });
            }
        }
    }
}
