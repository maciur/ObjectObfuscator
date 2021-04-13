using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Abstracts.Attributes;
using ObjectObfuscator.Abstracts.Definitions;
using ObjectObfuscator.Abstracts.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ObjectObfuscator
{
    public class Obfuscator : IObfuscator
    {
        private readonly int _maxDeepLevel;
        private readonly IPropertyHandler _propertyHandler;
        private readonly ITypeDetectHandler _typeDetectHandler;

        public Obfuscator(
            ITypeDetectHandler typeDetectHandler,
            IPropertyHandler propertyHandler, 
            int maxDeepLevel)
        {
            _typeDetectHandler = typeDetectHandler;
            _propertyHandler = propertyHandler;

            if (maxDeepLevel < 0)
                throw new ArgumentNullException(nameof(maxDeepLevel));

            _maxDeepLevel = maxDeepLevel;
        }

        public TObject Handle<TObject>(TObject @object) where TObject : class
        {
            return (TObject)Handle((object)@object);
        }

        public object Handle(object @object)
        {
            return Handle(@object, 1);
        }

        protected TValue[] Array<TValue>(TValue[] array, int deepLevel)
        {
            if (array == null || array.Length == 0)
                return array;

            foreach (var item in array)
            {
                Handle(item, deepLevel);
            }

            return array;
        }

        protected TClass Class<TClass>(TClass @object, int deepLevel) where TClass : class
        {
            if (@object == null)
                return @object;

            var type = typeof(TClass);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var classAttribute = type.GetCustomAttribute<OperationAttribute>();

            foreach (var property in properties)
            {
                var typeDefinition = _typeDetectHandler.Handle(property.PropertyType);

                if (typeDefinition.Type == ObjectType.Other)
                {
                    var attribute = property.GetCustomAttribute<OperationAttribute>();
                    OperationTypes? operation = attribute != null ? attribute.Type : classAttribute?.Type;

                    if (operation.HasValue)
                    {
                        _propertyHandler.Handle(operation.Value, property, @object);
                    }
                }
                else
                {
                    @object = (TClass)Handle(@object, deepLevel, typeDefinition);
                }
            }

            return @object;
        }

        protected IDictionary<TKey, TValue> Dictionary<TKey, TValue>(IDictionary<TKey, TValue> dictionary, int deepLevel)
        {
            if (dictionary == null || dictionary.Count == 0)
                return dictionary;

            foreach (var item in dictionary)
            {
                Handle(item.Value, deepLevel);
            }

            return dictionary;
        }

        protected IEnumerable<TValue> Enumerable<TValue>(IEnumerable<TValue> collection, int deepLevel)
        {
            if (collection == null || !collection.Any())
                return collection;

            foreach (var item in collection)
            {
                Handle(item, deepLevel);
            }

            return collection;
        }

        protected object Handle(object @object, int deepLevel, TypeDefinition typeDefinition = default)
        {
            if (@object == null || deepLevel > _maxDeepLevel)
                return @object;

            typeDefinition ??= _typeDetectHandler.Handle(@object.GetType());

            switch (typeDefinition.Type)
            {
                case ObjectType.Dictionary:
                    {
                        var dictionaryDefition = (DictionaryDefinition)typeDefinition;

                        var genericMethod = GetGenericMethod("Dictionary",
                            dictionaryDefition.GenericKeyArgument, dictionaryDefition.GenericValueArgument);

                        return genericMethod?.Invoke(this, new[] { @object, deepLevel });
                    }
                case ObjectType.Collection:
                    {
                        var collectionDefition = (CollectionDefinition)typeDefinition;

                        var genericMethod = GetGenericMethod("Enumerable", collectionDefition.GenericArgument);

                        return genericMethod?.Invoke(this, new[] { @object, deepLevel });
                    }

                case ObjectType.Array:
                    {
                        var arrayDefition = (ArrayDefinition)typeDefinition;

                        var genericMethod = GetGenericMethod("Array", arrayDefition.GenericArgument);

                        return genericMethod?.Invoke(this, new[] { @object, deepLevel });
                    }
                case ObjectType.Class:
                    {
                        var genericMethod = GetGenericMethod("Class", @object.GetType());

                        return genericMethod?.Invoke(this, new[] { @object, deepLevel + 1 });
                    }
                case ObjectType.Struct:
                    {
                        var genericMethod = GetGenericMethod("Struct", @object.GetType());

                        return genericMethod?.Invoke(this, new[] { @object, deepLevel + 1 });
                    }
                default: return @object;
            }
        }

        protected TStruct Struct<TStruct>(TStruct @object, int deepLevel)
                                    where TStruct : struct
        {
            var type = typeof(TStruct);
            var boxedStruct = (object)@object;
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var classAttribute = type.GetCustomAttribute<OperationAttribute>();

            foreach (var property in properties)
            {
                var typeDefinition = _typeDetectHandler.Handle(property.PropertyType);

                if (typeDefinition.Type == ObjectType.Other)
                {
                    var attribute = property.GetCustomAttribute<OperationAttribute>();
                    OperationTypes? operation = attribute != null ? attribute.Type : classAttribute?.Type;

                    if (operation.HasValue)
                    {
                        _propertyHandler.Handle(operation.Value, property, boxedStruct);
                    }
                }
                else
                {
                    boxedStruct = Handle(boxedStruct, deepLevel, typeDefinition);
                }
            }

            return (TStruct)boxedStruct;
        }

        private MethodInfo GetGenericMethod(string name, params Type[] parameters)
        {
            var methodInfo = GetType().GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
            return methodInfo.MakeGenericMethod(parameters);
        }
    }
}