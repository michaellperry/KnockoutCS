
namespace KnockoutCS.Example.Projection
{
    public class PersonSummary
    {
        private readonly Person _person;

        public PersonSummary(Person person)
        {
            _person = person;
        }

        public Person Person
        {
            get { return _person; }
        }

        public string Name
        {
            get { return _person.FirstName + " " + _person.LastName; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            PersonSummary that = obj as PersonSummary;
            if (that == null)
                return false;
            return _person.Equals(that._person);
        }

        public override int GetHashCode()
        {
            return _person.GetHashCode();
        }
    }
}
