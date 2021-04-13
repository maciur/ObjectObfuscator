using System;

namespace ObjectObfuscator.Abstracts.Definitions
{
    public class DictionaryDefinition : TypeDefinition
    {
        public Type GenericKeyArgument { get; }
        public Type GenericValueArgument { get; }

        public bool IsGeneric => GenericKeyArgument != null && GenericValueArgument != null;

        public DictionaryDefinition()
            : base(ObjectType.Dictionary)
        {
        }

        public DictionaryDefinition(
            Type genericKeyArgument, 
            Type genericValueArgument)
            : this()
        {
            GenericKeyArgument = genericKeyArgument;
            GenericValueArgument = genericValueArgument;
        }
    }
}