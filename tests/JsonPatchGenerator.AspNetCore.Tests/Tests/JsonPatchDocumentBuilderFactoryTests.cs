using JsonPatchGenerator.AspNetCore.Tests.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using System;
using Xunit;

namespace JsonPatchGenerator.AspNetCore.Tests.Tests
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

        [Fact]
        public void CanCreateGenericBuilder() =>
            TestGenericBuilderCreate<Box>(patchDocument => Assert.NotNull(patchDocument));


        [Fact]
        public void CreatedGenericBuilderHasCorrectType() =>
            TestGenericBuilderCreate<Box>(result => Assert.True(result is JsonPatchDocumentBuilder<Box>));

        private void TestGenericBuilderCreate<T>(Action<IPatchDocumentBuilder<IPatchDocument>> assert)
        {
            // arrange
            var target = new JsonPatchDocumentBuilderFactory();

            // act
            var result = target.Create<T>();

            // assert
            assert(result);
        }
    }
}
