using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UpdateControls;
using UpdateControls.Collections;

namespace KnockoutCS.Impl
{
    public static class ObservableTypeBuilder
    {
        private static readonly ConstructorInfo Independent_Ctor = typeof(Independent).GetConstructor(new Type[0]);
        private static readonly MethodInfo Independent_OnGet = typeof(Independent).GetMethod("OnGet");
        private static readonly MethodInfo Independent_OnSet = typeof(Independent).GetMethod("OnSet");

        private static ModuleBuilder _moduleBuilder;
        private static Dictionary<Type, TypeBuilder> _typeBuilderByModelType = new Dictionary<Type, TypeBuilder>();

        public static Type CreateType(Type modelType)
        {
            return GetTypeBuilder(modelType).CreateType();
        }

        private static ModuleBuilder GetModuleBuilder()
        {
            if (_moduleBuilder == null)
                _moduleBuilder = AppDomain.CurrentDomain
                    .DefineDynamicAssembly(new AssemblyName("KOGeneratedTypes"), AssemblyBuilderAccess.Run)
                    .DefineDynamicModule("KOModule");
            return _moduleBuilder;
        }

        private static TypeBuilder GetTypeBuilder(Type modelType)
        {
            TypeBuilder typeBuilder;
            if (_typeBuilderByModelType.TryGetValue(modelType, out typeBuilder))
                return typeBuilder;

            ModuleBuilder moduleBuilder = GetModuleBuilder();
            typeBuilder = moduleBuilder.DefineType(
                modelType.FullName + "__Observable",
                TypeAttributes.Class | TypeAttributes.Public);
            typeBuilder.SetParent(modelType);
            _typeBuilderByModelType.Add(modelType, typeBuilder);

            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                CallingConventions.HasThis,
                null);

            ILGenerator constructorGenerator = constructorBuilder.GetILGenerator();

            PropertyInfo[] properties = modelType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (!IsList(property))
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
            }

            constructorGenerator.Emit(OpCodes.Ldarg_0);
            constructorGenerator.Emit(OpCodes.Call, modelType.GetConstructor(new Type[0]));

            foreach (PropertyInfo property in properties)
            {
                if (IsList(property))
                {
                    Type childType = property.PropertyType.GetGenericArguments()[0];
                    Type independentListType = typeof(IndependentList<>).MakeGenericType(childType);

                    constructorGenerator.Emit(OpCodes.Ldarg_0);
                    constructorGenerator.Emit(OpCodes.Newobj, independentListType.GetConstructor(new Type[0]));
                    constructorGenerator.Emit(OpCodes.Call, property.GetSetMethod());
                }
            }


            constructorGenerator.Emit(OpCodes.Ret);
            return typeBuilder;
        }

        private static bool IsList(PropertyInfo property)
        {
            return
                property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(IList<>);
        }

        private static MethodBuilder CreateGetMethod(TypeBuilder typeBuilder, PropertyInfo property, FieldInfo independentField)
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

        private static MethodBuilder CreateSetMethod(TypeBuilder typeBuilder, PropertyInfo property, FieldInfo independentField)
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
