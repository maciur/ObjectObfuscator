using ObjectObfuscator.Abstracts.Definitions;
using ObjectObfuscator.Abstracts.Handlers;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ObjectObfuscator.Handlers
{
    public class DictionaryTypeHandler : TypeDetectHandler
    {
        private readonly Type[] _dictionaries;
        private readonly Type[] _generic;
        private readonly Type[] _readonly;

        public DictionaryTypeHandler(CollectionTypeHandler collectionTypeHandler)
            : base(collectionTypeHandler)
        {
            _generic = new[]
            {
                typeof(Dictionary<,>),
                typeof(SortedList<,>),
                typeof(SortedDictionary<,>),
                typeof(ConcurrentDictionary<,>),
                typeof(KeyedCollection<,>),
                typeof(KeyValuePair<,>),
            };

            _readonly = new[]
            {
                typeof(ReadOnlyDictionary<,>),
                typeof(ImmutableDictionary<,>),
                typeof(ImmutableSortedDictionary<,>),
            };

            _dictionaries = new[]
            {
                typeof(StringDictionary),
                typeof(NameValueCollection),
                typeof(SortedList),
            };
        }

        public override TypeDefinition Handle(Type type)
        {
            return (CheckGeneric(type) ?? Check(type, false, _dictionaries)) ?? base.Handle(type);
        }

        protected override TypeDefinition Create(bool isGeneric, Type type)
        {
            if (isGeneric)
            {
                var argumentsTypes = type.GetGenericArguments();
                return new DictionaryDefinition(argumentsTypes[0], argumentsTypes[1]);
            }

            return new DictionaryDefinition();
        }

        private TypeDefinition CheckGeneric(Type type)
        {
            return Check(type, type.IsGenericType, _generic) ?? Check(type, type.IsGenericType, _readonly);
        }
    }
}