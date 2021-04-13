using System;
using System.Reflection;

namespace ObjectObfuscator.Abstracts.Handlers
{
    public interface IPropertyHandler
    {
        //object Handle(OperationTypes operation, Type propertyType, object parent);
        void Handle(OperationTypes operation, PropertyInfo propertyInfo, object parent);
    }
}
