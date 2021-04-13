using ObjectObfuscator.Abstracts.Definitions;
using ObjectObfuscator.Abstracts.Handlers;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ObjectObfuscator.Handlers
{
    public class CollectionTypeHandler : TypeDetectHandler
    {
        private readonly Type[] _collections;
        private readonly Type[] _generic;
        private readonly Type[] _readonly;

        public CollectionTypeHandler(ArrayTypeHandler arrayTypeHandler)
            : base(arrayTypeHandler)
        {
            _generic = new[]
            {
                typeof(List<>),
                typeof(LinkedList<>),
                typeof(ObservableCollection<>),
                typeof(HashSet<>),
                typeof(SortedSet<>),
                typeof(Queue<>),
                typeof(Stack<>),
                typeof(ConcurrentQueue<>),
                typeof(ConcurrentStack<>),
            };

            _readonly = new[]
            {
                typeof(ImmutableList<>),
                typeof(ImmutableQueue<>),
                typeof(ImmutableStack<>),
                typeof(ImmutableSortedSet<>),
                typeof(ImmutableHashSet<>)
            };

            _collections = new[]
            {
                typeof(StringCollection),
                typeof(ArrayList),
                typeof(Queue),
                typeof(Stack),
            };
        }

        public override TypeDefinition Handle(Type type)
        {
            TypeDefinition typeDefinition = default;

            if (TypeIsCollection(type))
            {
                typeDefinition = CheckGeneric(type) ?? Check(type, false, _collections);
            }

            if (typeDefinition == null && type.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();

                if (typeof(LinkedListNode<>).IsAssignableFrom(genericType))
                {
                    var argumentsTypes = genericType.GetGenericArguments();
                    typeDefinition = new CollectionDefinition(argumentsTypes[0]);
                }
            }

            return typeDefinition ?? base.Handle(type);
        }

        protected override TypeDefinition Create(bool isGeneric, Type type)
        {
            if (isGeneric)
            {
                var argumentsTypes = type.GetGenericArguments();
                return new CollectionDefinition(argumentsTypes[0]);
            }

            return new CollectionDefinition();
        }

        private TypeDefinition CheckGeneric(Type type)
        {
            return Check(type, type.IsGenericType, _generic) ?? Check(type, type.IsGenericType, _readonly);
        }

        private bool TypeIsCollection(Type type)
        {
            if (typeof(IEnumerable).IsAssignableFrom(type))
                return true;

            if (typeof(ICollection).IsAssignableFrom(type))
                return true;

            if (type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition().IsAssignableFrom(typeof(IEnumerable<>))))
                return true;

            return false;
        }
    }
}
