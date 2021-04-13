using System;

namespace ObjectObfuscator.Abstracts.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class OperationAttribute : Attribute
    {
        public OperationTypes Type { get; }

        public OperationAttribute(OperationTypes type)
        {
            Type = type;
        }
    }
}