using JsonPatchGenerator.Core.Tests.Helpers;
using JsonPatchGenerator.Core.Tests.Models;
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
    }
}
