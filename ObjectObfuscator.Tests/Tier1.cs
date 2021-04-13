using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Abstracts.Attributes;

namespace ObjectObfuscator.Tests
{
    public class Tier1
    {
        [Operation(OperationTypes.Obfuscate)]
        public string Value { get; set; }
    }
}
