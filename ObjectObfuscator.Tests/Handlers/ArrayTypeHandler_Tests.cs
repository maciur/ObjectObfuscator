using FluentAssertions;
using NUnit.Framework;
using ObjectObfuscator.Abstracts.Definitions;
using ObjectObfuscator.Handlers;
using System;

namespace ObjectObfuscator.Tests.Handlers
{
    [TestFixture]
    public class ArrayTypeHandler_Tests
    {
        [TestCase(typeof(int[]), true)]
        [TestCase(typeof(int[,]), true)]
        [TestCase(typeof(int[,,]), true)]
        public void Correct(Type type, bool isGeneric)
        {
            var handler = new ArrayTypeHandler(null);
            var typeDefinition = handler.Handle(type);

            typeDefinition.Should().NotBeNull();
            typeDefinition.Should().BeOfType<ArrayDefinition>();

            var arrayDefinition = (ArrayDefinition)typeDefinition;

            arrayDefinition.IsGeneric.Should().Be(isGeneric);

            if (arrayDefinition.IsGeneric)
            {
                arrayDefinition.GenericArgument.Should().NotBeNull();
            }
        }

        [TestCase(typeof(string))]
        [TestCase(typeof(int))]
        [TestCase(typeof(long))]
        public void Incorrect(Type type)
        {
            var handler = new ArrayTypeHandler(null);
            var typeDefinition = handler.Handle(type);

            typeDefinition.Should().NotBeNull();
            typeDefinition.Should().BeOfType<TypeDefinition>();
        }
    }
}
