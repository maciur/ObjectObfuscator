using System;
using System.Reflection;

namespace ObjectObfuscator.Extensions
{
    internal static class PropertyInfoExtensions
    {
        internal static TAttribute GetCustomAttribute<TAttribute>(this PropertyInfo property) 
            where TAttribute : Attribute
        {
            var customAttributes = property.GetCustomAttributes(typeof(TAttribute), false);
            if (customAttributes != null && customAttributes.Length > 0 && customAttributes[0] is TAttribute customAttribute)
            {
                return customAttribute;
            }
            return null;
        }
    }
}
