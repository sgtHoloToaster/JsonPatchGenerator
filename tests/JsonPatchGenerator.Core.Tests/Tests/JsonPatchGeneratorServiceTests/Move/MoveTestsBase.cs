using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Enums;
using System;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    using static BaseTestsHelper;

    public class MoveTestsBase
    {
        readonly Func<JsonPatchGeneratorService> _getTarget;

        public MoveTestsBase(Func<JsonPatchGeneratorService> getTarget)
        {
            _getTarget = getTarget;
        }

        public void SupportSimpleTypeArrayMoveOperation() =>
            TestSimpleTypeArrayMoveOperation(HasMoveOperation);

        public void SimpleTypeArrayMoveOperationHasCorrectPath() =>
            TestSimpleTypeArrayMoveOperation(HasCorrectPath);

        public void SimpleTypeArrayMoveOperationHasCorrectValue() =>
            TestSimpleTypeArrayMoveOperation(HasCorrectValue);

        public void SimpleTypeArrayMoveOperationHasCorrectFrom() =>
            TestSimpleTypeArrayMoveOperation(HasCorrectFrom);

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

            var target = _getTarget();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, expectedPath, null, expectedFrom);
        }

        private void HasMoveOperation(IPatchDocument result) =>
            HasOperation(result, OperationType.Move);

        private void HasMoveOperation(IPatchDocument result, string path, object newValue, string from) =>
            HasMoveOperation(result);
    }
}
