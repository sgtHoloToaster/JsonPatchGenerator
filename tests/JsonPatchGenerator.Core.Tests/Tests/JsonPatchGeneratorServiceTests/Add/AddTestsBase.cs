using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Enums;
using System;
using System.Collections.Generic;

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
            var result = target.Generate(first, second);

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
            var result = target.Generate(first, second);

            // assert
            assert(result, expectedPath, addedValue);
        }

        private void HasAddOperation(IPatchDocument result) =>
            HasOperation(result, OperationType.Add);

        private void HasAddOperation(IPatchDocument result, string path, object newValue) =>
            HasAddOperation(result);

        public void SupportSimpleTypeListAddOperation() =>
            TestSimpleTypeListAddOperation(HasAddOperation);

        public void SimpleTypeListAddOperationHasCorrectPath() =>
            TestSimpleTypeListAddOperation(HasCorrectPath);

        public void SimpleTypeListAddOperationHasCorrectValue() =>
            TestSimpleTypeListAddOperation(HasCorrectValue);

        private void TestSimpleTypeListAddOperation(AssertAction assert)
        {
            // arrange
            const int addedValue = 139;
            var first = new ComplexPropertiesModel { SimpleTypeList = new List<int> { 4, 19, 85 } };
            var second = new ComplexPropertiesModel { SimpleTypeList = new List<int> { 4, 19, 85, addedValue } };
            var expectedPath = $"/{nameof(ComplexPropertiesModel.SimpleTypeList)}/-";

            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            assert(result, expectedPath, addedValue);
        }

        public void SupportSimpleTypeListIndexBasedAddOperation() =>
            TestSimpleTypeListIndexBasedAddOperation(HasAddOperation);

        public void SimpleTypeListIndexBasedOperationHasCorrectValue() =>
            TestSimpleTypeListIndexBasedAddOperation(HasCorrectValue);

        public void SimpleTypeListIndexBasedOperationHasCorrectPath() =>
            TestSimpleTypeListIndexBasedAddOperation(HasCorrectPath);

        private void TestSimpleTypeListIndexBasedAddOperation(AssertAction assert)
        {
            // arrange
            const int addedValue = 42;
            var first = new ComplexPropertiesModel { SimpleTypeList = new List<int> { 1, 91, 104 } };
            var second = new ComplexPropertiesModel { SimpleTypeList = new List<int> { 1, addedValue, 91, 104 } };
            var expectedPath = $"/{nameof(ComplexPropertiesModel.SimpleTypeList)}/{second.SimpleTypeList.IndexOf(addedValue)}";
            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            assert(result, expectedPath, addedValue);
        }
    }
}
