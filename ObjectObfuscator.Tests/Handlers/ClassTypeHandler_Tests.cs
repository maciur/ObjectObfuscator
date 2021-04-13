using FluentAssertions;
using NUnit.Framework;
using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Handlers;
using System;

namespace ObjectObfuscator.Tests.Handlers
{
    [TestFixture]
    public class ClassTypeHandler_Tests
    {
        [TestCase(typeof(Entity))]
        public void Correct(Type type)
        {
            var handler = new ClassTypeHandler();
            var typeDefinition = handler.Handle(type);

            typeDefinition.Should().NotBeNull();
            typeDefinition.Type.Should().BeEquivalentTo(ObjectType.Class);
        }
        
        [TestCase(typeof(EntityStruct))]
        [TestCase(typeof(int))]
        [TestCase(typeof(string))]
        public void Incorrect(Type type)
        {
            var handler = new ClassTypeHandler();
            var typeDefinition = handler.Handle(type);

            typeDefinition.Should().NotBeNull();
            typeDefinition.Type.Should().BeEquivalentTo(ObjectType.Other);
        }
    }
}
