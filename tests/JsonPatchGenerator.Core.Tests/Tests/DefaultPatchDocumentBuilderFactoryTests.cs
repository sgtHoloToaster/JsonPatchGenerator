using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using System;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class DefaultPatchDocumentBuilderFactoryTests
    {
        [Fact]
        public void CanCreateBuilder() =>
            TestBuilderCreate(Assert.NotNull);

        [Fact]
        public void CreatedBuilderHasCorrectType() =>
            TestBuilderCreate(result => Assert.True(result is DefaultPatchDocumentBuilder));

        private void TestBuilderCreate(Action<IPatchDocumentBuilder<IPatchDocument>> assert)
        {
            // arrange
            var target = new DefaultPatchDocumentBuilderFactory();

            // act
            var result = target.Create();

            // assert
            assert(result);
        }
    }
}
