using System;
using System.Diagnostics;
using System.Windows;
using UpdateControls;
using UpdateControls.Fields;

namespace KnockoutCS.Library1
{
    public class Computed<T>
    {
        private BaseViewModel _viewModel;
        private Dependent<T> _dependent;
        private string _propertyName;

        private Dependent _depFirePropertyChanged;

        public Computed(BaseViewModel viewModel, Func<T> calculation)
        {
            _viewModel = viewModel;
            _dependent = new Dependent<T>(calculation);
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
                return _dependent.Value;
            }
        }

        public static implicit operator T(Computed<T> computed)
        {
            return computed.Value;
        }

        private void RaisePropertyChanged()
        {
            if (_propertyName != null)
            {
                _viewModel.RaisePropertyChanged(_propertyName);
            }
            else
            {
                T value = _dependent.Value;
            }
        }
    }
}
