using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using ObjectObfuscator.Handlers;
using System;
using System.Collections.Generic;

namespace ObjectObfuscator.Tests
{
    [TestFixture]
    public class Obfuscator_Tests
    {
        private Obfuscator _obfuscator;

        [OneTimeSetUp]
        public void Init()
        {
            var classHandler = new ClassTypeHandler();
            var structHandler = new StructTypeHandler(classHandler);
            var arrayHandler = new ArrayTypeHandler(structHandler);
            var collectionHandler = new CollectionTypeHandler(arrayHandler);
            var dictionaryHandler = new DictionaryTypeHandler(collectionHandler);

            var removeValueHandler = new RemoveValueHandler();
            var obfuscatorHandler = new ObfuscateValueHandler(removeValueHandler, new StringValueHandler());

            _obfuscator = new Obfuscator(dictionaryHandler, obfuscatorHandler);
        }

        [Test]
        public void Simple_Class()
        {
            var entity = new Entity
            {
                Name = Guid.NewGuid().ToString("N"),
                String = Guid.NewGuid().ToString("N")
            };

            var clonedEntity = entity.Clone();

            _ = _obfuscator.Handle(entity);

            entity.Name.Should().NotBeEquivalentTo(clonedEntity.Name);
            entity.String.Should().BeEquivalentTo(clonedEntity.String);
        }
        
        [Test, AutoData]
        public void Simple_Struct(string randomName)
        {
            var entity = new EntityStruct
            {
                Name = randomName,
            };

            entity = (EntityStruct)_obfuscator.Handle(entity);

            entity.Name.Should().NotBeEquivalentTo(randomName);
        }
        
        [Test]
        public void Simple_Class_All_Properties()
        {
            var entity = new AllPropertiesEntity
            {
                Name = Guid.NewGuid().ToString("N"),
                String = Guid.NewGuid().ToString("N"),
                Number = int.MaxValue
            };

            var clonedEntity = entity.Clone<AllPropertiesEntity>();

            _ = _obfuscator.Handle(entity);

            entity.Name.Should().NotBeEquivalentTo(clonedEntity.Name);
            entity.Number.Should().NotBe(clonedEntity.Number);
            entity.String.Should().BeNull();
        }

        [Test]
        public void Class_In_Collection()
        {
            var entity = new Entity
            {
                Name = Guid.NewGuid().ToString("N"),
                String = Guid.NewGuid().ToString("N")
            };

            var clonedEntity = entity.Clone();

            var collection = _obfuscator.Handle(new List<Entity> { entity });

            collection[0].Name.Should().NotBeEquivalentTo(clonedEntity.Name);
            collection[0].String.Should().BeEquivalentTo(clonedEntity.String);
        }
        
        [Test]
        public void Diffrent_Classes_In_Collection()
        {
            var entityOne = new Entity
            {
                Name = Guid.NewGuid().ToString("N"),
                String = Guid.NewGuid().ToString("N")
            };

            var entityTwo = new NumberEntity
            {
                Name = Guid.NewGuid().ToString("N"),
                String = Guid.NewGuid().ToString("N"),
                Number = int.MaxValue
            };

            var clonedEntityOne = entityOne.Clone();
            var clonedEntityTwo = entityTwo.Clone<NumberEntity>();

            var collection = _obfuscator.Handle(new List<Entity> { entityOne, entityTwo });

            collection[0].Name.Should().NotBeEquivalentTo(clonedEntityOne.Name);
            collection[0].String.Should().BeEquivalentTo(clonedEntityOne.String);

            collection[1].Name.Should().NotBeEquivalentTo(clonedEntityTwo.Name);
            collection[1].String.Should().BeEquivalentTo(clonedEntityTwo.String);

            ((NumberEntity)collection[1]).Number.Should().NotBe(int.MaxValue);
        } 
        
        [Test]
        public void Class_In_Array()
        {
            var entity = new Entity
            {
                Name = Guid.NewGuid().ToString("N"),
                String = Guid.NewGuid().ToString("N")
            };

            var clonedEntity = entity.Clone();

            var array = _obfuscator.Handle(new Entity[] { entity });

            array[0].Name.Should().NotBeEquivalentTo(clonedEntity.Name);
            array[0].String.Should().BeEquivalentTo(clonedEntity.String);
        }
        
        [Test, AutoData]
        public void Class_In_Dictionary(string key)
        {
            var entity = new Entity
            {
                Name = Guid.NewGuid().ToString("N"),
                String = Guid.NewGuid().ToString("N")
            };

            var clonedEntity = entity.Clone();

            var collection = _obfuscator.Handle(new Dictionary<string, Entity> { { key, entity } });

            collection[key].Name.Should().NotBeEquivalentTo(clonedEntity.Name);
            collection[key].String.Should().BeEquivalentTo(clonedEntity.String);
        }

        [Test, AutoData]
        public void MaxDeepLevel(string randomValue)
        {
            var tier2 = new Tier2
            {
                Value = randomValue
            };
            tier2.Tier = tier2;

            tier2 = _obfuscator.Handle(tier2);

            tier2.Should().NotBeNull();
            tier2.Tier.Should().NotBeNull();
            tier2.Tier.Value.Should().NotBeEquivalentTo(randomValue);
        }
    }
}
