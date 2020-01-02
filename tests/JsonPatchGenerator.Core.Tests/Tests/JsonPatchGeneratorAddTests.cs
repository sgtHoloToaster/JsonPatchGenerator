using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models;
using System;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class JsonPatchGeneratorAddTests : JsonPatchGeneratorTests
    {
        public JsonPatchGeneratorAddTests() : base() { }

        [Fact]
        public void SupportSimpleTypeArrayAddOperation() =>
            TestSimpleTypeArrayAddOperation(HasAddOperation);

        [Fact]
        public void SimpleTypeArrayAddOperationHasCorrectPath() =>
            TestSimpleTypeArrayAddOperation(HasCorrectPath);

        [Fact]
        public void SimpleTypeArrayAddOperationHasCorrectValue() =>
            TestSimpleTypeArrayAddOperation(HasCorrectValue);

        private void TestSimpleTypeArrayAddOperation(AssertAction assert)
        {
            // arrange
            const int addedValue = 4;
            var first = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 2, 3 } };
            var second = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 2, 3, addedValue } };
            var expectedPath = $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/-";

            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, expectedPath, addedValue);
        }

        [Fact]
        public void SupportSimpleTypeArrayIndexBasedAddOperation() =>
            TestSimpleTypeArrayIndexBasedAddOperation(HasAddOperation);

        [Fact]
        public void SimpleTypeArrayIndexBasedAddDoesntProduceExtraOperations() =>
            TestSimpleTypeArrayIndexBasedAddOperation(HasNoExtraOperations);

        [Fact]
        public void SimpleTypeArrayIndexBasedAddOperationHasCorrectValue() =>
            TestSimpleTypeArrayIndexBasedAddOperation(HasCorrectValue);

        [Fact]
        public void SimpleTypeArrayIndexBasedAddOperationHasCorrectPath() =>
            TestSimpleTypeArrayIndexBasedAddOperation(HasCorrectPath);

        private void TestSimpleTypeArrayIndexBasedAddOperation(AssertAction assert)
        {
            // arrange
            const int addedValue = 2;
            var first = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 3, 4 } };
            var second = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, addedValue, 3, 4 } };
            var expectedPath = $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/{Array.IndexOf(second.SimpleTypeArray, addedValue)}";
            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, expectedPath, addedValue);
        }

        private void HasAddOperation(DiffDocument result) =>
            HasOperation(result, OperationType.Add);

        private void HasAddOperation(DiffDocument result, string path, object newValue) =>
            HasAddOperation(result);
    }
}
