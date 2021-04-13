using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Abstracts.Handlers;
using System.Reflection;

namespace ObjectObfuscator.Handlers
{
    public class RemoveValueHandler : PropertyHandler
    {
        private readonly MethodInfo _defaultValueMethod;

        public RemoveValueHandler()
        {
            _defaultValueMethod = GetType().GetMethod(nameof(DefaultValue));
        }

        public TValue DefaultValue<TValue>()
        {
            return default;
        }

        public override void Handle(OperationTypes operation, PropertyInfo propertyInfo, object parent)
        {
            switch (operation)
            {
                case OperationTypes.Remove:
                    UpdateValue(propertyInfo, parent, (type, @object) =>
                    {
                        var genericMethod = _defaultValueMethod.MakeGenericMethod(type);
                        return genericMethod.Invoke(this, new object[0]);
                    });
                    break;

                default:
                    base.Handle(operation, propertyInfo, parent);
                    break;
            }
        }
    }
}