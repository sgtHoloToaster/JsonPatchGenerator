using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Enums;
using System;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    using static BaseTestsHelper;

    public class AddTestsBase
    {
        readonly Func<JsonPatchGeneratorService> _getTarget;

        public AddTestsBase(Func<JsonPatchGeneratorService> getTarget)
        {
            _getTarget = getTarget;
        }

        public void SupportSimpleTypeArrayAddOperation() =>
            TestSimpleTypeArrayAddOperation(HasAddOperation);

        public void SimpleTypeArrayAddOperationHasCorrectPath() =>
            TestSimpleTypeArrayAddOperation(HasCorrectPath);

        public void SimpleTypeArrayAddOperationHasCorrectValue() =>
            TestSimpleTypeArrayAddOperation(HasCorrectValue);

        private void TestSimpleTypeArrayAddOperation(AssertAction assert)
        {
            // arrange
            const int addedValue = 4;
            var first = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 2, 3 } };
            var second = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 2, 3, addedValue } };
            var expectedPath = $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/-";

            var target = _getTarget();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, expectedPath, addedValue);
        }

        public void SupportSimpleTypeArrayIndexBasedAddOperation() =>
            TestSimpleTypeArrayIndexBasedAddOperation(HasAddOperation);

        public void SimpleTypeArrayIndexBasedAddDoesntProduceExtraOperations() =>
            TestSimpleTypeArrayIndexBasedAddOperation(HasNoExtraOperations);

        public void SimpleTypeArrayIndexBasedAddOperationHasCorrectValue() =>
            TestSimpleTypeArrayIndexBasedAddOperation(HasCorrectValue);

        public void SimpleTypeArrayIndexBasedAddOperationHasCorrectPath() =>
            TestSimpleTypeArrayIndexBasedAddOperation(HasCorrectPath);

        private void TestSimpleTypeArrayIndexBasedAddOperation(AssertAction assert)
        {
            // arrange
            const int addedValue = 2;
            var first = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 3, 4 } };
            var second = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, addedValue, 3, 4 } };
            var expectedPath = $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/{Array.IndexOf(second.SimpleTypeArray, addedValue)}";
            var target = _getTarget();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, expectedPath, addedValue);
        }

        private void HasAddOperation(IPatchDocument result) =>
            HasOperation(result, OperationType.Add);

        private void HasAddOperation(IPatchDocument result, string path, object newValue) =>
            HasAddOperation(result);
    }
}
