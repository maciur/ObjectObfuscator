namespace ObjectObfuscator.Abstracts.Definitions
{
    public class TypeDefinition
    {
        public ObjectType Type { get; }

        public TypeDefinition(ObjectType type)
        {
            Type = type;
        }
    }
}