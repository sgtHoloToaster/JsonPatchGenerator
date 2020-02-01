using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Enums;
using System;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class JsonPatchGeneratorRemoveTests : JsonPatchGeneratorTests
    {
        public JsonPatchGeneratorRemoveTests() : base() { }

        [Fact]
        public void SupportSimpleTypeArrayRemoveOperation() =>
            TestSimpleTypeArrayRemoveOperation(HasRemoveOperation);

        [Fact]
        public void SimpleTypeArrayRemoveOperationHasCorrectPath() =>
            TestSimpleTypeArrayRemoveOperation(HasCorrectPath);

        [Fact]
        public void SimpleTypeArrayRemoveOperationHasCorrectValue() =>
            TestSimpleTypeArrayRemoveOperation(HasCorrectValue);

        [Fact]
        public void SimpleTypeArrayMoveDoesntProduceExtraOperations() =>
            TestSimpleTypeArrayRemoveOperation(HasNoExtraOperations);

        private void TestSimpleTypeArrayRemoveOperation(AssertAction assert)
        {
            // arrange
            const int removedValue = 2;
            var first = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, removedValue, 3, 4,  } };
            var second = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 3, 4 } };
            var expectedPath = $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/{Array.IndexOf(first.SimpleTypeArray, removedValue)}";

            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, expectedPath, removedValue);
        }

        private void HasRemoveOperation(IPatchDocument result) =>
            HasOperation(result, OperationType.Remove);

        private void HasRemoveOperation(IPatchDocument result, string path, object newValue) =>
            HasRemoveOperation(result);
    }
}
