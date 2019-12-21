using AutoMoqCore;
using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Helpers;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Services;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public partial class JsonPatchGeneratorTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();

        public JsonPatchGeneratorTests()
        {
            _mocker.GetMock<ITypeResolver>()
                   .Setup(m => m.GetProperties(It.IsAny<Type>()))
                   .Returns<Type>(t => t.GetProperties().Select(p => new ObjectProperty(p)));

            _mocker.GetMock<ITypeResolver>()
                   .Setup(m => m.GetValue(It.IsAny<object>(), It.IsAny<ObjectProperty>()))
                   .Returns<object, ObjectProperty>((obj, prop) => obj.GetType().GetProperty(prop.Name).GetValue(obj));
        }

        [Fact]
        public void SupportReplaceOperationForSimpleTypes()
        {
            // arrange & act
            var (result, _) = TestReplaceSimpleTypeOperation();

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Operations);
            Assert.Contains(result.Operations, o => o.Type == OperationType.Replace);
        }

        [Fact]
        public void ReplaceOperationHasCorrectValue()
        {
            // arrange & act
            var (result, changedValue) = TestReplaceSimpleTypeOperation();

            // assert
            Assert.NotNull(result);
            var replaceOperation = result.Operations.FirstOrDefault(o => o.Type == OperationType.Replace);
            Assert.Equal(changedValue, replaceOperation.Value);
        }

        [Fact]
        public void DontCreateExtraOperationsOnReplace()
        {
            // arrange & act
            var (result, _) = TestReplaceSimpleTypeOperation();

            // assert
            Assert.NotNull(result?.Operations);
            Assert.Single(result.Operations);
        }

        private (DiffDocument result, object changedValue) TestReplaceSimpleTypeOperation()
        {
            // arrange
            var first = new SimpleTypesPublicPropertiesModel();
            var second = new SimpleTypesPublicPropertiesModel();
            var property = first.GetType().GetProperty(nameof(SimpleTypesPublicPropertiesModel.IntProperty));
            const int value = 42;
            property.SetValue(first, value);
            var changedValue = value + 1;
            property.SetValue(second, changedValue);
            var target = _mocker.Create<JsonPatchGeneratorService>();

            // act
            return (target.GetDiff(first, second), changedValue);
        }

        [Fact]
        public void SupportReplaceOperationsForNestedObjects()
        {
            // arrange
            const int initValue = 42;
            var changedValue = initValue + 1;
            ComplexPropertiesModel createTestObject(int value) =>
                new ComplexPropertiesModel
                {
                    ComplexTypeProperty = new ComplexPropertiesModel
                    {
                        ComplexTypeProperty = new ComplexPropertiesModel
                        {
                            SimpleTypeProperty = value
                        }
                    }
                };

            var first = createTestObject(initValue);
            var second = createTestObject(changedValue);
            var target = _mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            Assert.NotNull(result?.Operations);
            Assert.NotEmpty(result.Operations);
            Assert.Contains(result.Operations, o => o.Type == OperationType.Replace);
        }

        [Fact]
        public void CreateCorrectPathForNestedPropertiesOnReplace()
        {
            // arrange
            var complexTypePropertyName = nameof(ComplexPropertiesModel.ComplexTypeProperty);
            var simpleTypePropertyName = nameof(ComplexPropertiesModel.SimpleTypeProperty);
            var path = $"/{complexTypePropertyName}/{complexTypePropertyName}/{complexTypePropertyName}/{simpleTypePropertyName}";
            const int initValue = 441;
            var first = new ComplexPropertiesModel();
            PropertiesPathfinder.SetValue(first, path, initValue);
            var second = new ComplexPropertiesModel();
            var changedValue = initValue + 551;
            PropertiesPathfinder.SetValue(second, path, changedValue);
            var target = _mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            Assert.NotNull(result?.Operations);
            var operation = result.Operations.FirstOrDefault(o => o.Type == OperationType.Replace);
            Assert.Equal(path, operation.Path);
        }

        [Fact]
        public void ReplaceOperationForComplexPropertiesHasCorrectValue()
        {
            // arrange
            var first = new ComplexPropertiesModel();
            var value = new ComplexPropertiesModel { SimpleTypeProperty = 123 };
            var second = new ComplexPropertiesModel { ComplexTypeProperty = value };
            var target = _mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            Assert.NotNull(result);
            Assert.Equal(value, result.Operations.First().Value);
        }

        [Fact]
        public void SupportReplacingValuesWithNull()
        {
            // arrange
            var first = new ComplexPropertiesModel { ComplexTypeProperty = new ComplexPropertiesModel { SimpleTypeProperty = 123 } };
            var second = new ComplexPropertiesModel();
            var target = _mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            Assert.NotNull(result);
            Assert.Null(result.Operations.First().Value);
        }

        [Fact]
        public void SupportSimpleTypeArrayElementReplacing()
        {
            // arrange & act
            var (result, _, _) = TestSimpleTypeArrayElementReplacing();

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Operations.Where(o => o.Type == OperationType.Replace));
        }

        [Fact]
        public void SimpleTypeArrayElementReplaceOperationHasCorrectValue()
        {
            // arrange & act
            var (result, _, newValue) = TestSimpleTypeArrayElementReplacing();

            // assert
            Assert.NotNull(result);
            var operation = result.Operations.First();
            Assert.Equal(newValue, operation.Value);
        }

        [Fact]
        public void ArrayElementReplaceOperationHasCorrectPath()
        {
            // arrange & act
            var (result, valuePath, _) = TestSimpleTypeArrayElementReplacing();

            // assert
            Assert.NotNull(result);
            var operation = result.Operations.First();
            Assert.Equal(valuePath, operation.Path);
        }

        private (DiffDocument result, string changedValuePath, int newValue) TestSimpleTypeArrayElementReplacing()
        {
            // arrange
            var initialArray = new int[] { 1, 2, 3 };
            const int newValue = 7;
            const int changedValueIndex = 1;
            var first = new ComplexPropertiesModel { SimpleTypeArray = initialArray.Clone() as int[] };
            var second = new ComplexPropertiesModel { SimpleTypeArray = initialArray.Clone() as int[] };
            var changedValuePath = $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}[{changedValueIndex}]";
            PropertiesPathfinder.SetValue(second, changedValuePath, newValue);
            var target = _mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            return (result, changedValuePath, newValue);
        }

        [Fact]
        public void SupportReplacingPropertiesOfArrayElement()
        {
            // arrange & act
            var (result, _, _) = TestComplexTypeArrayElementPropertyReplace();

            // assert
            Assert.NotNull(result?.Operations);
            Assert.NotEmpty(result.Operations);
            Assert.Contains(result.Operations, o => o.Type == OperationType.Replace);
        }

        [Fact]
        public void ArrayElementPropertyReplaceOperationHasCorrectValue()
        {
            // arrange & act
            var (result, _, newValue) = TestComplexTypeArrayElementPropertyReplace();

            // assert
            Assert.NotNull(result);
            var operation = result.Operations.First();
            Assert.Equal(newValue, operation.Value);
        }

        [Fact]
        public void ArrayElementPropertyReplaceOperationHasCorrectPath()
        {
            // arrange & act
            var (result, path, _) = TestComplexTypeArrayElementPropertyReplace();

            // assert
            Assert.NotNull(result);
            var operation = result.Operations.First();
            Assert.Equal(path, operation.Path);
        }

        private (DiffDocument result, string valuePath, int changedValue) TestComplexTypeArrayElementPropertyReplace()
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
            var changedValuePath = $"/{nameof(ComplexPropertiesModel.ComplexTypeArrayProperty)}[{changedValueIndex}]/{nameof(ComplexPropertiesModel.SimpleTypeProperty)}";
            PropertiesPathfinder.SetValue(second, changedValuePath, newValue);
            var target = _mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            return (result, changedValuePath, newValue);
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

            var target = _mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Operations.Count());
        }
    }
}
