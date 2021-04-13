using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Abstracts.Attributes;
using System;

namespace ObjectObfuscator.Tests
{
    public class Entity<TCollection> : Entity
    {
        public TCollection Childs { get; set; }
    }

    public class NumberEntity : Entity
    {
        [Operation(OperationTypes.Remove)]
        public int Number { get; set; }
    }

    public class Entity : IComparable
    {
        [Operation(OperationTypes.Obfuscate)]
        public string Name { get; set; }

        public string String { get; set; }

        public int CompareTo(object obj)
        {
            if (!(obj is Entity entity))
                return -1;

            return entity.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;
        }

        public Entity Clone()
        {
            return (Entity)MemberwiseClone();
        }
        
        public TEntity Clone<TEntity>()
            where TEntity : Entity
        {
            return (TEntity)MemberwiseClone();
        }
    }
}
