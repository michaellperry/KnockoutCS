/**********************************************************************
 * 
 * Update Controls .NET
 * Copyright 2010 Michael L Perry
 * MIT License
 * 
 * http://updatecontrols.net
 * http://updatecontrols.codeplex.com/
 * 
 **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Threading;
using System.Reflection;

namespace KnockoutCS.Impl
{
    public class ObjectInstance<TModel, TViewModel> : ICustomTypeProvider, IObjectInstance, INotifyPropertyChanged, IDataErrorInfo, IEditableObject
    {
        // Wrap the class and all of its property definitions.
		private static ClassInstance _classInstance = new ClassInstance(typeof(TModel), typeof(TViewModel), typeof(ObjectInstance<TModel, TViewModel>));

        // Wrap the model and view model.
        private object _model;
        private object _viewModel;

        // The dispatcher for the view that I'm attached to.
        private Dispatcher _dispatcher;

		// Wrap all properties.
        private Dictionary<ClassProperty, ObjectProperty> _properties = new Dictionary<ClassProperty, ObjectProperty>();

		public ObjectInstance(object model, object viewModel, Dispatcher dispatcher)
		{
            _model = model;
			_viewModel = viewModel;
            _dispatcher = dispatcher;
		}

        public ClassInstance ClassInstance
        {
            get { return _classInstance; }
        }

        public object Model
        {
            get { return _model; }
        }

        public object ViewModel
        {
            get { return _viewModel; }
        }

        public Dispatcher Dispatcher
        {
            get { return _dispatcher; }
        }

        public ObjectProperty LookupProperty(ClassProperty classProperty)
        {
            ObjectProperty objectProperty;
            if (!_properties.TryGetValue(classProperty, out objectProperty))
            {
                objectProperty = ObjectProperty.From(this, classProperty);
                _properties.Add(classProperty, objectProperty);
            }
			return objectProperty;
		}

        public override string ToString()
        {
            return _viewModel.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj == this)
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            ObjectInstance<TModel, TViewModel> that = (ObjectInstance<TModel, TViewModel>)obj;
            return Object.Equals(this._viewModel, that._viewModel);
        }

        public override int GetHashCode()
        {
            return _viewModel.GetHashCode();
        }

        public void FirePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

		public string Error
		{
			get
			{
				IDataErrorInfo wrappedObject = _viewModel as IDataErrorInfo;
				return wrappedObject != null ? wrappedObject.Error : null;
			}
		}

		public string this[string columnName]
		{
			get
			{
				IDataErrorInfo wrappedObject = _viewModel as IDataErrorInfo;
				return wrappedObject != null ? wrappedObject[columnName] : null;
			}
		}

        #region IEditableObject Members

        public void BeginEdit()
        {
            IEditableObject wrappedObject = _viewModel as IEditableObject;
            if (wrappedObject != null)
                wrappedObject.BeginEdit();
        }

        public void CancelEdit()
        {
            IEditableObject wrappedObject = _viewModel as IEditableObject;
            if (wrappedObject != null)
                wrappedObject.CancelEdit();
        }

        public void EndEdit()
        {
            IEditableObject wrappedObject = _viewModel as IEditableObject;
            if (wrappedObject != null)
                wrappedObject.EndEdit();
        }

        #endregion

        public Type GetCustomType()
        {
            return new ClassInstance(typeof(TModel), typeof(TViewModel), typeof(ObjectInstance<TModel, TViewModel>));
        }
    }
}
