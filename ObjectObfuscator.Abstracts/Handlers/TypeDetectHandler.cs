using ObjectObfuscator.Abstracts.Definitions;
using System;

namespace ObjectObfuscator.Abstracts.Handlers
{
    public abstract class TypeDetectHandler : ITypeDetectHandler
    {
        private readonly TypeDetectHandler _nextHandler;

        protected TypeDetectHandler(TypeDetectHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public TypeDetectHandler()
        {
        }

        public virtual TypeDefinition Handle(Type type)
        {
            if(_nextHandler != null)
            {
                return _nextHandler.Handle(type);
            }
            return new TypeDefinition(ObjectType.Other);
        }

        protected TypeDefinition Check(Type type, bool isGeneric, Type[] types)
        {
            TypeDefinition typeDefinition = default;

            int index = 0;

            var typeToCheck = isGeneric ? type.GetGenericTypeDefinition() : type;

            do
            {
                if (typeToCheck.IsAssignableFrom(types[index]))
                {
                    typeDefinition = Create(isGeneric, type);
                }
                index++;
            }
            while (typeDefinition == default && types.Length > index);

            return typeDefinition;
        }

        protected abstract TypeDefinition Create(bool isGeneric, Type type);
    }
}
