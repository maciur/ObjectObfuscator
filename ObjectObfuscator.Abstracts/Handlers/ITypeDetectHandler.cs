using ObjectObfuscator.Abstracts.Definitions;
using System;

namespace ObjectObfuscator.Abstracts.Handlers
{
    public interface ITypeDetectHandler
    {
        TypeDefinition Handle(Type type);
    }
}
