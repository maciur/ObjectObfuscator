using FluentAssertions;
using NUnit.Framework;
using ObjectObfuscator.Abstracts.Definitions;
using ObjectObfuscator.Handlers;
using System;

namespace ObjectObfuscator.Tests.Handlers
{
    [TestFixture]
    public class CollectionTypeHandler_Tests
    {
        [TestCaseSource(typeof(TestCasesSource<CollectionTestCases>), nameof(TestCasesSource<CollectionTestCases>.TestCases))]
        public void Correct(Type type, bool isGeneric)
        {
            var handler = new CollectionTypeHandler(null);
            var typeDefinition = handler.Handle(type);

            typeDefinition.Should().NotBeNull();
            typeDefinition.Should().BeOfType<CollectionDefinition>();

            var collectionDefinition = (CollectionDefinition)typeDefinition;

            collectionDefinition.IsGeneric.Should().Be(isGeneric);

            if (collectionDefinition.IsGeneric)
            {
                collectionDefinition.GenericArgument.Should().NotBeNull();
            }
        }
    }
}
