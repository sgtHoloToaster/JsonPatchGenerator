using JsonPatchGenerator.Core.Tests.Helpers;
using JsonPatchGenerator.Core.Tests.Models;
using System.Collections.Generic;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class PropertiesPathfinderTests
    {
        [Fact]
        public void InitializeComplexObjectsWhenTryingToSetNestedValue()
        {
            // arrange
            var model = new ComplexPropertiesModel();
            var complexTypePropertyName = nameof(ComplexPropertiesModel.ComplexType);
            var simpleTypePropertyName = nameof(ComplexPropertiesModel.SimpleType);
            var path = $"/{complexTypePropertyName}/{complexTypePropertyName}/{complexTypePropertyName}/{simpleTypePropertyName}";
            const int value = 42;

            // act
            PropertiesPathfinder.SetValue(model, path, value);

            // assert
            Assert.NotNull(model.ComplexType?.ComplexType?.ComplexType);
        }

        [Fact]
        public void CanSetNestedSimpleTypeProperty()
        {
            // arrange
            var model = new ComplexPropertiesModel { ComplexType = new ComplexPropertiesModel { ComplexType = new ComplexPropertiesModel() } };
            const int value = 14;
            var complexTypePropertyName = nameof(ComplexPropertiesModel.ComplexType);
            var simpleTypePropertyName = nameof(ComplexPropertiesModel.SimpleType);
            var path = $"/{complexTypePropertyName}/{complexTypePropertyName}/{complexTypePropertyName}/{simpleTypePropertyName}";

            // act
            PropertiesPathfinder.SetValue(model, path, value);

            // assert
            Assert.Equal(value, model.ComplexType.ComplexType.ComplexType.SimpleType);
        }

        [Fact]
        public void CanSetArrayElementByIndex()
        {
            // arrange
            var model = new ComplexPropertiesModel { SimpleTypeArray = new int[] { 1, 2, 3 } };
            const int changedValueIndex = 1;
            const int newValue = 531;
            var path = $"{nameof(ComplexPropertiesModel.SimpleTypeArray)}/{changedValueIndex}";

            // act
            PropertiesPathfinder.SetValue(model, path, newValue);

            // assert
            Assert.Equal(newValue, model.SimpleTypeArray[changedValueIndex]);
        }

        [Fact]
        public void CanSetListElementByIndex()
        {
            // arrange
            var model = new ComplexPropertiesModel { SimpleTypeList = new List<int> { -910, 2, 10442 } };
            const int changedValueIndex = 1;
            const int newValue = 531;
            var path = $"{nameof(ComplexPropertiesModel.SimpleTypeList)}/{changedValueIndex}";

            // act
            PropertiesPathfinder.SetValue(model, path, newValue);

            // assert
            Assert.Equal(newValue, model.SimpleTypeList[changedValueIndex]);
        }

        [Fact]
        public void CanSetPropertiesOfComplexTypeArrayElement()
        {
            // arrange
            var model = new ComplexPropertiesModel
            {
                ComplexTypeArray = new ComplexPropertiesModel[] 
                {
                    new ComplexPropertiesModel(),
                    new ComplexPropertiesModel(),
                    new ComplexPropertiesModel()
                }
            };

            const int changedValueIndex = 1;
            const int newValue = 531;
            var path = $"{nameof(ComplexPropertiesModel.ComplexTypeArray)}/{changedValueIndex}/{nameof(ComplexPropertiesModel.SimpleType)}";

            // act
            PropertiesPathfinder.SetValue(model, path, newValue);

            // assert
            Assert.Equal(newValue, model.ComplexTypeArray[changedValueIndex].SimpleType);
        }

        [Fact]
        public void CanSetPropertiesOfComplexTypeListElement()
        {
            // arrange
            var model = new ComplexPropertiesModel
            {
                ComplexTypeList = new List<ComplexPropertiesModel>
                {
                    new ComplexPropertiesModel(),
                    new ComplexPropertiesModel(),
                    new ComplexPropertiesModel()
                }
            };

            const int changedValueIndex = 1;
            const int newValue = 1001;
            var path = $"{nameof(ComplexPropertiesModel.ComplexTypeList)}/{changedValueIndex}/{nameof(ComplexPropertiesModel.SimpleType)}";

            // act
            PropertiesPathfinder.SetValue(model, path, newValue);

            // assert
            Assert.Equal(newValue, model.ComplexTypeList[changedValueIndex].SimpleType);
        }
    }
}
