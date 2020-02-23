using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Enums;
using System;
using System.Collections.Generic;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    using static BaseTestsHelper;

    public class RemoveTestsBase
    {
        readonly Func<JsonPatchGeneratorService> _getTarget;

        public RemoveTestsBase(Func<JsonPatchGeneratorService> getTarget) 
        {
            _getTarget = getTarget;
        }

        public void SupportSimpleTypeArrayRemoveOperation() =>
            TestSimpleTypeArrayRemoveOperation(HasRemoveOperation);

        public void SimpleTypeArrayRemoveOperationHasCorrectPath() =>
            TestSimpleTypeArrayRemoveOperation(HasCorrectPath);

        public void SimpleTypeArrayRemoveOperationHasCorrectValue() =>
            TestSimpleTypeArrayRemoveOperation(HasCorrectValue);

        public void SimpleTypeArrayMoveDoesntProduceExtraOperations() =>
            TestSimpleTypeArrayRemoveOperation(HasNoExtraOperations);

        private void TestSimpleTypeArrayRemoveOperation(AssertAction assert)
        {
            // arrange
            const int removedValue = 2;
            var first = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, removedValue, 3, 4, } };
            var second = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 3, 4 } };
            var expectedPath = $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/{Array.IndexOf(first.SimpleTypeArray, removedValue)}";
            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            assert(result, expectedPath, null);
        }

        public void SupportSimpleTypeListRemoveOperation() =>
            TestSimpleTypeListRemoveOperation(HasRemoveOperation);

        public void SimpleTypeListRemoveOperationHasCorrectPath() =>
            TestSimpleTypeListRemoveOperation(HasCorrectPath);

        public void SimpleTypeListRemoveOperationHasCorrectValue() =>
            TestSimpleTypeListRemoveOperation(HasCorrectValue);

        public void SimpleTypeListMoveDoesntProduceExtraOperations() =>
            TestSimpleTypeListRemoveOperation(HasNoExtraOperations);

        private void TestSimpleTypeListRemoveOperation(AssertAction assert)
        {
            // arrange
            const int removedValue = 2;
            var first = new ComplexPropertiesModel { SimpleTypeList = new List<int> { -5, removedValue, 3, -101, } };
            var second = new ComplexPropertiesModel { SimpleTypeList = new List<int> { -5, 3, -101 } };
            var expectedPath = $"/{nameof(ComplexPropertiesModel.SimpleTypeList)}/{first.SimpleTypeList.IndexOf(removedValue)}";
            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            assert(result, expectedPath, null);
        }

        private void HasRemoveOperation(IPatchDocument result) =>
            HasOperation(result, OperationType.Remove);

        private void HasRemoveOperation(IPatchDocument result, string path, object newValue) =>
            HasRemoveOperation(result);
    }
}
