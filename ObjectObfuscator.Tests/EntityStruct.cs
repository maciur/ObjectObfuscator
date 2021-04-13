using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Abstracts.Attributes;

namespace ObjectObfuscator.Tests
{
    public struct EntityStruct
    {
        [Operation(OperationTypes.Obfuscate)]
        public string Name { get; set; }

        public EntityStruct Clone()
        {
            return (EntityStruct)MemberwiseClone();
        }
    }
}
