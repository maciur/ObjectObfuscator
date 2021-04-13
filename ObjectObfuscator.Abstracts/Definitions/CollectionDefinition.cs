using System;

namespace ObjectObfuscator.Abstracts.Definitions
{
    public class CollectionDefinition : TypeDefinition
    {
        public Type GenericArgument { get; }

        public bool IsGeneric => GenericArgument != null;

        public CollectionDefinition()
            : base(ObjectType.Collection)
        {
        }

        public CollectionDefinition(Type genericArgument)
            : this()
        {
            GenericArgument = genericArgument;
        }
    }
}