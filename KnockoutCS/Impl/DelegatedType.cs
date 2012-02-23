using System;
using System.Globalization;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace KnockoutCS.Impl
{
    public abstract class DelegatedType : Type
    {
        private Type _modelType;
        private Type _viewModelType;
        private Dictionary<string, PropertyInfo> _propertyByName = new Dictionary<string,PropertyInfo>();

        public DelegatedType(Type modelType, Type viewModelType)
        {
            _modelType = modelType;
            _viewModelType = viewModelType;
        }

        protected abstract PropertyInfo DelegatePropertyInfo(PropertyInfo rawPropertyInfo, bool isModelProperty);

        public override Assembly Assembly
        {
            get { return _viewModelType.Assembly; }
        }

        public override string AssemblyQualifiedName
        {
            get { return _viewModelType.AssemblyQualifiedName; }
        }

        public override Type BaseType
        {
            get { return _viewModelType.BaseType; }
        }

        public override string FullName
        {
            get { return _viewModelType.FullName; }
        }

        public override Guid GUID
        {
            get { return _viewModelType.GUID; }
        }

        protected override TypeAttributes GetAttributeFlagsImpl()
        {
            return new TypeAttributes();
        }

        protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
            return _viewModelType.GetConstructor(bindingAttr, binder, types, modifiers);
        }

        public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            return _viewModelType.GetConstructors(bindingAttr);
        }

        public override Type GetElementType()
        {
            return _viewModelType.GetElementType();
        }

        public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
        {
            return _viewModelType.GetEvent(name, bindingAttr);
        }

        public override EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            return _viewModelType.GetEvents(bindingAttr);
        }

        public override FieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            return _viewModelType.GetField(name, bindingAttr);
        }

        public override FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            return _viewModelType.GetFields(bindingAttr);
        }

        public override Type GetInterface(string name, bool ignoreCase)
        {
            return _viewModelType.GetInterface(name, ignoreCase);
        }

        public override Type[] GetInterfaces()
        {
            return _viewModelType.GetInterfaces();
        }

        public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            return _viewModelType.GetMembers(bindingAttr);
        }

        protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
        {
            return _viewModelType.GetMethod(name, bindingAttr, binder, types, modifiers);
        }

        public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            return _viewModelType.GetMethods(bindingAttr);
        }

        public override Type GetNestedType(string name, BindingFlags bindingAttr)
        {
            return _viewModelType.GetNestedType(name, bindingAttr);
        }

        public override Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            return _viewModelType.GetNestedTypes(bindingAttr);
        }

        public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            PropertyInfo[] viewModelProperties = _viewModelType.GetProperties(bindingAttr);
            PropertyInfo[] modelProperties = _modelType.GetProperties(bindingAttr);
            var properties = viewModelProperties.Union(modelProperties
                .Where(modelProperty => !viewModelProperties.Any(viewModelProperty =>
                    viewModelProperty.Name == modelProperty.Name)));
            return viewModelProperties.ToArray();
        }

        protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            PropertyInfo result;
            if (!_propertyByName.TryGetValue(name, out result))
            {
                PropertyInfo propertyInfo = _viewModelType.GetProperty(name, bindingAttr);
                bool isModelProperty = false;
                if (propertyInfo == null)
                {
                    propertyInfo = _modelType.GetProperty(name, bindingAttr);
                    isModelProperty = true;
                    if (propertyInfo == null)
                        return null;
                }
                result = DelegatePropertyInfo(propertyInfo, isModelProperty);
                _propertyByName.Add(name, result);
            }
            return result;
        }

        protected override bool HasElementTypeImpl()
        {
            return _viewModelType.HasElementType;
        }

        public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
        {
            if (_viewModelType.GetMember(name, invokeAttr) != null)
                return _viewModelType.InvokeMember(name, invokeAttr, binder, target, args);
            else
                return _modelType.InvokeMember(name, invokeAttr, binder, target, args);
        }

        protected override bool IsArrayImpl()
        {
            return _viewModelType.IsArray;
        }

        protected override bool IsByRefImpl()
        {
            return _viewModelType.IsByRef;
        }

        protected override bool IsCOMObjectImpl()
        {
            return _viewModelType.IsCOMObject;
        }

        protected override bool IsPointerImpl()
        {
            return _viewModelType.IsPointer;
        }

        protected override bool IsPrimitiveImpl()
        {
            return _viewModelType.IsPrimitive;
        }

        public override Module Module
        {
            get { return _viewModelType.Module; }
        }

        public override string Namespace
        {
            get { return _viewModelType.Namespace; }
        }

        public override Type UnderlyingSystemType
        {
            get { return _viewModelType.UnderlyingSystemType; }
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _viewModelType.GetCustomAttributes(attributeType, inherit);
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return _viewModelType.GetCustomAttributes(inherit);
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return _viewModelType.IsDefined(attributeType, inherit);
        }

        public override string Name
        {
            get { return _viewModelType.Name; }
        }
    }
}
