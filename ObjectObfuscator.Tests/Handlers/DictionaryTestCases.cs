using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ObjectObfuscator.Tests.Handlers
{
    public class DictionaryTestCases
    {
        public object[] Test1() => new object[] { typeof(StringDictionary), false };
        public object[] Test2() => new object[] { typeof(NameValueCollection), false };
        public object[] Test3() => new object[] { typeof(SortedList), false };
        public object[] Test4() => new object[] { typeof(Dictionary<int, string>), true };
        public object[] Test5() => new object[] { typeof(SortedList<int, string>), true };
        public object[] Test6() => new object[] { typeof(SortedDictionary<int, string>), true };
        public object[] Test7() => new object[] { typeof(ConcurrentDictionary<int, string>), true };
        public object[] Test8() => new object[] { typeof(KeyedCollection<int, string>), true };
        public object[] Test9() => new object[] { typeof(KeyValuePair<int, string>), true };
        public object[] Test10() => new object[] { typeof(ReadOnlyDictionary<int, string>), true };
        public object[] Test11() => new object[] { typeof(ImmutableDictionary<int, string>), true };
        public object[] Test12() => new object[] { typeof(ImmutableSortedDictionary<int, string>), true };
    }
}