using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models;
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

        private void TestSimpleTypeArrayMoveOperation(MoveAssertAction assert)
        {
            // arrange
            const int movedValue = 2;
            var first = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 3, 4, movedValue } };
            var second = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, movedValue, 3, 4 } };
            string GetValuePath(ComplexPropertiesModel model) =>
                $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/{Array.IndexOf(model.SimpleTypeArray, movedValue)}";
            var expectedPath = GetValuePath(first);
            var expectedFrom = GetValuePath(second);

            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, expectedPath, movedValue, expectedFrom);
        }

        private void HasMoveOperation(DiffDocument result) =>
            HasOperation(result, OperationType.Add);

        private void HasMoveOperation(DiffDocument result, string path, object newValue, string from) =>
            HasMoveOperation(result);
    }
}
