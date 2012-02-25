using System;

namespace KnockoutCS.Example.Projection
{
    public class PersonSummary
    {
        private readonly Person _person;

        public PersonSummary(Person person)
        {
            _person = person;
        }

        [KOIdentity]
        public Person Person
        {
            get { return _person; }
        }

        public string Name
        {
            get { return _person.FirstName + " " + _person.LastName; }
        }

        public static PersonSummary FromPerson(Person person)
        {
            return person == null
                ? null
                : new PersonSummary(person);
        }

        public static Person ToPerson(PersonSummary personSummary)
        {
            return personSummary == null
                ? null
                : personSummary.Person;
        }
    }
}
