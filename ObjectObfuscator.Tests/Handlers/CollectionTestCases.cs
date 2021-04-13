using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ObjectObfuscator.Tests.Handlers
{
    public class CollectionTestCases
    {
        public object[] Test14() => new object[] { typeof(ImmutableList<int>), true };

        public object[] Test1() => new object[] { typeof(StringCollection), false };

        public object[] Test10() => new object[] { typeof(Queue<int>), true };

        public object[] Test11() => new object[] { typeof(Stack<int>), true };

        public object[] Test12() => new object[] { typeof(ConcurrentQueue<int>), true };

        public object[] Test13() => new object[] { typeof(ConcurrentStack<int>), true };

        public object[] Test15() => new object[] { typeof(ImmutableQueue<int>), true };

        public object[] Test16() => new object[] { typeof(ImmutableStack<int>), true };

        public object[] Test17() => new object[] { typeof(ImmutableSortedSet<int>), true };

        public object[] Test18() => new object[] { typeof(ImmutableHashSet<int>), true };

        public object[] Test2() => new object[] { typeof(ArrayList), false };

        public object[] Test3() => new object[] { typeof(Queue), false };

        public object[] Test4() => new object[] { typeof(Stack), false };

        public object[] Test5() => new object[] { typeof(List<int>), true };

        public object[] Test6() => new object[] { typeof(LinkedList<int>), true };

        public object[] Test7() => new object[] { typeof(ObservableCollection<int>), true };

        public object[] Test8() => new object[] { typeof(HashSet<int>), true };

        public object[] Test9() => new object[] { typeof(SortedSet<int>), true };
    }
}