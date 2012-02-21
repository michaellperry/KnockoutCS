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
using System.Reflection;

namespace KnockoutCS.Library.Impl
{
    public class ClassInstance : DelegatedType
    {
        private Type _modelType;
        private Type _viewModelType;
        private Type _objectInstanceType;
        private List<ClassProperty> _classProperties;

        public ClassInstance(Type modelType, Type viewModelType, Type objectInstanceType)
            : base(modelType, viewModelType)
        {
            _modelType = modelType;
            _viewModelType = viewModelType;
            _objectInstanceType = objectInstanceType;

            // Create a wrapper for each non-collection property.
            PropertyInfo[] viewModelProperties = _viewModelType.GetProperties();
            _classProperties = viewModelProperties
                .Select(p => new ClassProperty(false, p, objectInstanceType))
                .Union(_modelType
                    .GetProperties()
                    .Where(modelProperty => !viewModelProperties.Any(viewModelProperty =>
                        viewModelProperty.Name == modelProperty.Name))
                    .Select(p => new ClassProperty(true, p, objectInstanceType)))
                .ToList();
        }

        public IEnumerable<ClassProperty> ClassProperties
        {
            get { return _classProperties; }
        }

        public override string ToString()
        {
            return "KO View Model for " + _modelType.Name;
        }

        protected override PropertyInfo DelegatePropertyInfo(PropertyInfo rawPropertyInfo, bool isModelProperty)
        {
            return new ClassProperty(isModelProperty, rawPropertyInfo, _objectInstanceType);
        }
    }
}
