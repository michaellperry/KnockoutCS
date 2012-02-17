using KnockoutCS.Library1;

namespace KnockoutCS.API1
{
    public class MainViewModel : BaseViewModel
    {
        private Observable<string> _firstName;
        private Observable<string> _lastName;
        private Computed<string> _fullName;

        public MainViewModel()
        {
            _firstName = new Observable<string>(this);
            _lastName = new Observable<string>(this);
            _fullName = new Computed<string>(this, () => _firstName + " " + _lastName);

            _firstName.Value = "Michael";
            _lastName.Value = "Perry";
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName.Value = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName.Value = value; }
        }

        public string FullName
        {
            get { return _fullName; }
        }
    }
}
