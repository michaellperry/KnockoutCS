using System;
using System.Reflection;
using System.Reflection.Emit;
using UpdateControls;

namespace KnockoutCS.Impl
{
    public class ObservableTypeBuilder
    {
        private static readonly ConstructorInfo Independent_Ctor = typeof(Independent).GetConstructor(new Type[0]);
        private static readonly MethodInfo Independent_OnGet = typeof(Independent).GetMethod("OnGet");
        private static readonly MethodInfo Independent_OnSet = typeof(Independent).GetMethod("OnSet");

        private static ModuleBuilder _moduleBuilder;

        private Type _modelType;

        public ObservableTypeBuilder(Type modelType)
        {
            _modelType = modelType;
        }

        public Type CreateType()
        {
            return CreateObservableType().CreateType();
        }

        private static ModuleBuilder GetModuleBuilder()
        {
            if (_moduleBuilder == null)
                _moduleBuilder = AppDomain.CurrentDomain
                    .DefineDynamicAssembly(new AssemblyName("KOGeneratedTypes"), AssemblyBuilderAccess.Run)
                    .DefineDynamicModule("KOModule");
            return _moduleBuilder;
        }

        private TypeBuilder CreateObservableType()
        {
            ModuleBuilder moduleBuilder = GetModuleBuilder();
            TypeBuilder typeBuilder = moduleBuilder.DefineType(_modelType.FullName + "__Observable", TypeAttributes.Class | TypeAttributes.Public);
            typeBuilder.SetParent(_modelType);

            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                CallingConventions.HasThis,
                null);

            ILGenerator constructorGenerator = constructorBuilder.GetILGenerator();

            PropertyInfo[] properties = _modelType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                FieldBuilder independentField = typeBuilder.DefineField(
                    "_ind" + property.Name,
                    typeof(Independent),
                    FieldAttributes.Private);

                PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(
                    property.Name,
                    PropertyAttributes.HasDefault,
                    property.PropertyType,
                    null);
                propertyBuilder.SetGetMethod(CreateGetMethod(typeBuilder, property, independentField));
                propertyBuilder.SetSetMethod(CreateSetMethod(typeBuilder, property, independentField));

                constructorGenerator.Emit(OpCodes.Ldarg_0);
                constructorGenerator.Emit(OpCodes.Newobj, Independent_Ctor);
                constructorGenerator.Emit(OpCodes.Stfld, independentField);
            }

            constructorGenerator.Emit(OpCodes.Ldarg_0);
            constructorGenerator.Emit(OpCodes.Call, _modelType.GetConstructor(new Type[0]));
            constructorGenerator.Emit(OpCodes.Ret);
            return typeBuilder;
        }

        private MethodBuilder CreateGetMethod(TypeBuilder typeBuilder, PropertyInfo property, FieldInfo independentField)
        {
            MethodBuilder getMethod = typeBuilder.DefineMethod(
                "get_" + property.Name,
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                property.PropertyType,
                null);

            ILGenerator getGenerator = getMethod.GetILGenerator();
            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.Emit(OpCodes.Ldfld, independentField);
            getGenerator.Emit(OpCodes.Callvirt, Independent_OnGet);
            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.Emit(OpCodes.Call, property.GetGetMethod());
            getGenerator.Emit(OpCodes.Ret);
            return getMethod;
        }

        private MethodBuilder CreateSetMethod(TypeBuilder typeBuilder, PropertyInfo property, FieldInfo independentField)
        {
            MethodBuilder setMethod = typeBuilder.DefineMethod(
                "set_" + property.Name,
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                null,
                new Type[] { property.PropertyType });

            ILGenerator setGenerator = setMethod.GetILGenerator();
            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldfld, independentField);
            setGenerator.Emit(OpCodes.Callvirt, Independent_OnSet);
            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldarg_1);
            setGenerator.Emit(OpCodes.Call, property.GetSetMethod());
            setGenerator.Emit(OpCodes.Ret);
            return setMethod;
        }
    }
}
