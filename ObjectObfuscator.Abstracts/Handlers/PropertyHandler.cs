using System;
using System.Reflection;

namespace ObjectObfuscator.Abstracts.Handlers
{
    public abstract class PropertyHandler : IPropertyHandler
    {
        private readonly PropertyHandler _next;

        protected PropertyHandler()
        {
        }

        protected PropertyHandler(PropertyHandler next)
        {
            _next = next;
        }

        public virtual void Handle(OperationTypes operation, PropertyInfo propertyInfo, object parent)
        {
            if (_next != null)
            {
                _next.Handle(operation, propertyInfo, parent);
            }
        }

        protected void UpdateValue(PropertyInfo propertyInfo, object parent, Func<Type, object, object> operation)
        {
            var propertyValue = propertyInfo.GetValue(parent);
            var propertyType = propertyInfo.PropertyType;

            if (propertyValue != null)
            {
                propertyValue = operation(propertyType, propertyValue);
                propertyInfo.SetValue(parent, propertyValue);
            }
        }
    }
}