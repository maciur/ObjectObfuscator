using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Abstracts.Attributes;

namespace ObjectObfuscator.Tests
{
    [Operation(OperationTypes.Remove)]
    public class AllPropertiesEntity : Entity
    {
        public int Number { get; set; }
    }
}
