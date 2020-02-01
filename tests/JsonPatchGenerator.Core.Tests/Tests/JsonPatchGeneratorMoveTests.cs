using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.JsonNet.Enums;
using System;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class JsonPatchGeneratorMoveTests : JsonPatchGeneratorTests
    {
        public JsonPatchGeneratorMoveTests() : base() { }

        [Fact]
        public void SupportSimpleTypeArrayMoveOperation() =>
            TestSimpleTypeArrayMoveOperation(HasMoveOperation);

        [Fact]
        public void SimpleTypeArrayMoveOperationHasCorrectPath() =>
            TestSimpleTypeArrayMoveOperation(HasCorrectPath);

        [Fact]
        public void SimpleTypeArrayMoveOperationHasCorrectValue() =>
            TestSimpleTypeArrayMoveOperation(HasCorrectValue);

        [Fact]
        public void SimpleTypeArrayMoveOperationHasCorrectFrom() =>
            TestSimpleTypeArrayMoveOperation(HasCorrectFrom);

        [Fact]
        public void SimpleTypeArrayMoveDoesntProduceExtraOperations() =>
            TestSimpleTypeArrayMoveOperation(HasNoExtraOperations);

        private void TestSimpleTypeArrayMoveOperation(MoveAssertAction assert)
        {
            // arrange
            const int movedValue = 2;
            var first = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 3, 4, movedValue } };
            var second = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, movedValue, 3, 4 } };
            string GetValuePath(ComplexPropertiesModel model) =>
                $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/{Array.IndexOf(model.SimpleTypeArray, movedValue)}";
            var expectedFrom = GetValuePath(first);
            var expectedPath = GetValuePath(second);

            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, expectedPath, movedValue, expectedFrom);
        }

        private void HasMoveOperation(IPatchDocument result) =>
            HasOperation(result, OperationType.Move);

        private void HasMoveOperation(IPatchDocument result, string path, object newValue, string from) =>
            HasMoveOperation(result);
    }
}
