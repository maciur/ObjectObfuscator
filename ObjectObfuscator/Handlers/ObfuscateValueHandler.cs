using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Abstracts.Handlers;
using System.Reflection;

namespace ObjectObfuscator.Handlers
{
    public class ObfuscateValueHandler : PropertyHandler
    {
        private readonly IStringValueHandler _stringValueHandler;

        public ObfuscateValueHandler(
            RemoveValueHandler next,
            IStringValueHandler stringValueHandler) : base(next)
        {
            _stringValueHandler = stringValueHandler;
        }

        public override void Handle(OperationTypes operation, PropertyInfo propertyInfo, object parent)
        {
            var propertyType = propertyInfo.PropertyType;
            var propertyTypeIsString = !propertyType.Equals(typeof(string));

            switch (operation)
            {
                case OperationTypes.Obfuscate when !propertyTypeIsString:
                    base.Handle(OperationTypes.Remove, propertyInfo, parent);
                    break;

                case OperationTypes.Obfuscate when propertyTypeIsString:
                    UpdateValue(propertyInfo, parent, (type, @object) => _stringValueHandler.Obfuscate((string)@object));
                    break;

                default:
                    base.Handle(operation, propertyInfo, parent);
                    break;
            }
        }
    }
}