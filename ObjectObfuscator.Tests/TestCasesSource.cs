using System.Linq;
using System.Reflection;

namespace ObjectObfuscator.Tests
{
    public static class TestCasesSource<TClass>
        where TClass : class, new()
    {
        public static object[] TestCases()
        {
            var cases = new TClass();
            var methods = typeof(TClass)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.DeclaringType.Namespace.StartsWith(typeof(TestCasesSource<>).Namespace))
                .ToArray();

            var result =  methods.Select(x => x.Invoke(cases, null)).ToArray();
            return result;
        }
    }
}
