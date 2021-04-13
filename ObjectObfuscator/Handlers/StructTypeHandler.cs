using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Abstracts.Definitions;
using ObjectObfuscator.Abstracts.Handlers;
using System;

namespace ObjectObfuscator.Handlers
{
    public class StructTypeHandler : TypeDetectHandler
    {
        public StructTypeHandler(ClassTypeHandler classTypeHandler) 
            : base(classTypeHandler)
        {
        }

        public override TypeDefinition Handle(Type type)
        {
            if(type.IsValueType && !type.IsEnum && !type.IsPrimitive && !type.Namespace.StartsWith("System."))
            {
                return Create(false, type);
            }

            return base.Handle(type);
        }

        protected override TypeDefinition Create(bool isGeneric, Type type)
        {
            return new TypeDefinition(ObjectType.Struct);
        }
    }
}
