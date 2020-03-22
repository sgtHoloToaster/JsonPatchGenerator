using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using System;
using Xunit;

namespace JsonPatchGenerator.Marvin.JsonPatch.Tests.Tests
{
    public class JsonPatchDocumentBuilderFactoryTests
    {
        [Fact]
        public void CanCreateBuilder() =>
            TestBuilderCreate(Assert.NotNull);

        [Fact]
        public void CreatedBuilderHasCorrectType() =>
            TestBuilderCreate(result => Assert.True(result is JsonPatchDocumentBuilder));

        private void TestBuilderCreate(Action<IPatchDocumentBuilder<IPatchDocument>> assert)
        {
            // arrange
            var target = new JsonPatchDocumentBuilderFactory();

            // act
            var result = target.Create();

            // assert
            assert(result);
        }
    }
}
