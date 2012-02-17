using System.Diagnostics;
using System.Windows;
using UpdateControls;
using UpdateControls.Fields;

namespace KnockoutCS.Library1
{
    public class Observable<T>
    {
        private BaseViewModel _viewModel;
        private Independent<T> _independent = new Independent<T>();
        private string _propertyName;

        private Dependent _depFirePropertyChanged;

        public Observable(BaseViewModel viewModel)
        {
            _viewModel = viewModel;
            _depFirePropertyChanged = new Dependent(RaisePropertyChanged);
            _depFirePropertyChanged.Invalidated += delegate
            {
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    _depFirePropertyChanged.OnGet();
                });
            };
            _depFirePropertyChanged.OnGet();
        }

        public T Value
        {
            get
            {
                if (_propertyName == null)
                {
                    StackFrame frame = new StackFrame(2);
                    string callingMethod = frame.GetMethod().Name;
                    if (callingMethod.StartsWith("get_"))
                    {
                        _propertyName = callingMethod.Substring(4);
                    }
                }
                return _independent.Value;
            }
            set { _independent.Value = value; }
        }

        public static implicit operator T(Observable<T> observable)
        {
            return observable.Value;
        }

        private void RaisePropertyChanged()
        {
            if (_propertyName != null)
            {
                _viewModel.RaisePropertyChanged(_propertyName);
            }
            else
            {
                T value = _independent.Value;
            }
        }
    }
}
