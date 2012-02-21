
namespace KnockoutCS
{
    public class ViewModel
    {
        private dynamic _model;

        public ViewModel(dynamic model)
        {
            _model = model;
        }

        public string FullName
        {
            get { return _model.FirstName + " " + _model.LastName; }
        }
    }
}
