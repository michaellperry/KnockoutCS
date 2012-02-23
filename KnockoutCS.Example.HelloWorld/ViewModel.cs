
namespace KnockoutCS.Example.HelloWorld
{
    public class ViewModel
    {
        private Model _model;

        public ViewModel(Model model)
        {
            _model = model;
        }

        public string FullName
        {
            get { return _model.FirstName + " " + _model.LastName; }
        }
    }
}
