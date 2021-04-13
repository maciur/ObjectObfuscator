using FluentAssertions;
using NUnit.Framework;
using ObjectObfuscator.Abstracts;
using ObjectObfuscator.Handlers;
using System;

namespace ObjectObfuscator.Tests.Handlers
{
    [TestFixture]
    public class StructTypeHandler_Tests
    {
        [TestCase(typeof(EntityStruct))]
        public void Correct(Type type)
        {
            var handler = new StructTypeHandler(null);
            var typeDefinition = handler.Handle(type);

            typeDefinition.Should().NotBeNull();
            typeDefinition.Type.Should().BeEquivalentTo(ObjectType.Struct);
        }
        
        [TestCase(typeof(Entity))]
        public void Incorrect(Type type)
        {
            var handler = new StructTypeHandler(null);
            var typeDefinition = handler.Handle(type);

            typeDefinition.Should().NotBeNull();
            typeDefinition.Type.Should().BeEquivalentTo(ObjectType.Other);
        }
    }
}
