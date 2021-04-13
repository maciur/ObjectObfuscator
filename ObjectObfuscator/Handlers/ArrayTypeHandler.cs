using ObjectObfuscator.Abstracts.Definitions;
using ObjectObfuscator.Abstracts.Handlers;
using System;
using System.Collections;

namespace ObjectObfuscator.Handlers
{
    public class ArrayTypeHandler : TypeDetectHandler
    {
        public ArrayTypeHandler(StructTypeHandler structTypeHandler)
            : base(structTypeHandler)
        {
        }

        public override TypeDefinition Handle(Type type)
        {
            if (!typeof(string).Equals(type) && typeof(IEnumerable).IsAssignableFrom(type))
                return Create(true, type.GetElementType());

            return base.Handle(type);
        }

        protected override TypeDefinition Create(bool isGeneric, Type type)
        {
            if(isGeneric && type != null)
            {
                return new ArrayDefinition(type);
            }
            return new ArrayDefinition();
        }
    }
}