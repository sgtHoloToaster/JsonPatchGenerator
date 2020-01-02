using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Helpers;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models;
using System.Linq;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class JsonPatchGeneratorReplaceTests : JsonPatchGeneratorTests
    {
        public JsonPatchGeneratorReplaceTests() : base() { }

        [Fact]
        public void SupportReplaceOperationForSimpleTypes() =>
            TestReplaceSimpleTypeOperation(HasReplaceOperation);

        [Fact]
        public void ReplaceOperationHasCorrectValue() =>
            TestReplaceSimpleTypeOperation(HasCorrectValue);

        [Fact]
        public void ReplaceOperationHasCorrectPath() =>
            TestReplaceSimpleTypeOperation(HasCorrectPath);

        [Fact]
        public void DontCreateExtraOperationsOnReplace() =>
            TestReplaceSimpleTypeOperation(HasNoExtraOperations);

        private void TestReplaceSimpleTypeOperation(AssertAction assert)
        {
            // arrange
            var first = new SimpleTypesPublicPropertiesModel();
            var second = new SimpleTypesPublicPropertiesModel();
            var propertyName = nameof(SimpleTypesPublicPropertiesModel.IntProperty);
            var property = first.GetType().GetProperty(propertyName);
            const int value = 42;
            property.SetValue(first, value);
            var path = $"/{propertyName}";
            var changedValue = value + 1;
            property.SetValue(second, changedValue);
            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, path, changedValue);
        }

        [Fact]
        public void SupportReplaceOperationsForNestedObjects() =>
            TestComplexTypeNestedPropertiesReplace(HasReplaceOperation);

        [Fact]
        public void CreateCorrectPathForNestedPropertiesOnReplace() =>
            TestComplexTypeNestedPropertiesReplace(HasCorrectPath);

        private void TestComplexTypeNestedPropertiesReplace(AssertAction assert)
        {
            // arrange
            var complexTypePropertyName = nameof(ComplexPropertiesModel.ComplexTypeProperty);
            var simpleTypePropertyName = nameof(ComplexPropertiesModel.SimpleTypeProperty);
            var path = $"/{complexTypePropertyName}/{complexTypePropertyName}/{complexTypePropertyName}/{simpleTypePropertyName}";
            const int initValue = 441;
            var first = new ComplexPropertiesModel();
            PropertiesPathfinder.SetValue(first, path, initValue);
            var second = new ComplexPropertiesModel();
            var newValue = initValue + 551;
            PropertiesPathfinder.SetValue(second, path, newValue);
            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, path, newValue);
        }

        [Fact]
        public void ReplaceOperationForComplexPropertiesHasCorrectValue()
        {
            // arrange
            var first = new ComplexPropertiesModel();
            var value = new ComplexPropertiesModel { SimpleTypeProperty = 123 };
            var second = new ComplexPropertiesModel { ComplexTypeProperty = value };
            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            HasCorrectValue(result, value);
        }

        [Fact]
        public void SupportReplacingValuesWithNull()
        {
            // arrange
            var first = new ComplexPropertiesModel { ComplexTypeProperty = new ComplexPropertiesModel { SimpleTypeProperty = 777 } };
            var second = new ComplexPropertiesModel();
            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            HasReplaceOperation(result);
        }

        [Fact]
        public void SupportSimpleTypeArrayElementReplacing() =>
            TestSimpleTypeArrayElementReplacing(HasReplaceOperation);

        [Fact]
        public void SimpleTypeArrayElementReplaceOperationHasCorrectValue() =>
            TestSimpleTypeArrayElementReplacing(HasCorrectValue);

        [Fact]
        public void ArrayElementReplaceOperationHasCorrectPath() =>
            TestSimpleTypeArrayElementReplacing(HasCorrectPath);

        private void TestSimpleTypeArrayElementReplacing(AssertAction assert)
        {
            // arrange
            var initialArray = new int[] { 1, 2, 3 };
            const int newValue = 7;
            const int changedValueIndex = 1;
            var first = new ComplexPropertiesModel { SimpleTypeArray = initialArray.Clone() as int[] };
            var second = new ComplexPropertiesModel { SimpleTypeArray = initialArray.Clone() as int[] };
            var changedValuePath = $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/{changedValueIndex}";
            PropertiesPathfinder.SetValue(second, changedValuePath, newValue);
            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, changedValuePath, newValue);
        }

        [Fact]
        public void SupportReplacingPropertiesOfArrayElement() =>
            TestComplexTypeArrayElementPropertyReplace(HasReplaceOperation);

        [Fact]
        public void ArrayElementPropertyReplaceOperationHasCorrectValue() =>
            TestComplexTypeArrayElementPropertyReplace(HasCorrectValue);

        [Fact]
        public void ArrayElementPropertyReplaceOperationHasCorrectPath() =>
            TestComplexTypeArrayElementPropertyReplace(HasCorrectPath);

        private void TestComplexTypeArrayElementPropertyReplace(AssertAction assert)
        {
            // arrange
            var initialModel = new ComplexPropertiesModel
            {
                ComplexTypeArrayProperty = new ComplexPropertiesModel[] 
                {
                    new ComplexPropertiesModel(),
                    new ComplexPropertiesModel { SimpleTypeProperty = 55 },
                    new ComplexPropertiesModel()
                }
            };

            var first = initialModel;
            var second = ObjectCloner.DeepClone(initialModel);
            const int newValue = 77;
            const int changedValueIndex = 1;
            var changedValuePath = $"/{nameof(ComplexPropertiesModel.ComplexTypeArrayProperty)}/{changedValueIndex}/{nameof(ComplexPropertiesModel.SimpleTypeProperty)}";
            PropertiesPathfinder.SetValue(second, changedValuePath, newValue);
            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, changedValuePath, newValue);
        }

        [Fact]
        public void CanHandleMultipleDifferences()
        {
            // arrange
            var first = new SimpleTypesPublicPropertiesModel();
            var second = new SimpleTypesPublicPropertiesModel
            {
                BoolProperty = true,
                ByteProperty = 1,
                IntProperty = 13
            };

            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Operations.Count());
        }

        private void HasReplaceOperation(DiffDocument result) =>
            HasOperation(result, OperationType.Replace);

        private void HasReplaceOperation(DiffDocument result, string path, object newValue) =>
            HasReplaceOperation(result);
    }
}
