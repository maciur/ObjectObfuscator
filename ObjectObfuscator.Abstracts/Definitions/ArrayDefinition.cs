using System;

namespace ObjectObfuscator.Abstracts.Definitions
{
    public class ArrayDefinition : TypeDefinition
    {
        public Type GenericArgument { get; }

        public bool IsGeneric => GenericArgument != null;

        public ArrayDefinition() : base(ObjectType.Array)
        {
        }

        public ArrayDefinition(Type genericArgument) : this()
        {
            GenericArgument = genericArgument;
        }
    }
}