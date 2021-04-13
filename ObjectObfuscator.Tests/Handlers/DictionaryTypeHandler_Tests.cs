using FluentAssertions;
using NUnit.Framework;
using ObjectObfuscator.Abstracts.Definitions;
using ObjectObfuscator.Handlers;
using System;

namespace ObjectObfuscator.Tests.Handlers
{
    [TestFixture]
    public class DictionaryTypeHandler_Tests
    {
        [TestCaseSource(typeof(TestCasesSource<DictionaryTestCases>), nameof(TestCasesSource<DictionaryTestCases>.TestCases))]
        public void Correct(Type type, bool isGeneric)
        {
            var handler = new DictionaryTypeHandler(null);
            var typeDefinition = handler.Handle(type);

            typeDefinition.Should().NotBeNull();
            typeDefinition.Should().BeOfType<DictionaryDefinition>();

            var dictionaryDefinition = (DictionaryDefinition)typeDefinition;

            dictionaryDefinition.IsGeneric.Should().Be(isGeneric);

            if (dictionaryDefinition.IsGeneric)
            {
                dictionaryDefinition.GenericKeyArgument.Should().NotBeNull();
                dictionaryDefinition.GenericKeyArgument.Should().NotBeNull();
            }
        }
    }
}
