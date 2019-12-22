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
            var complexTypePropertyName = nameof(ComplexPropertiesModel.ComplexTypeProperty);
            var simpleTypePropertyName = nameof(ComplexPropertiesModel.SimpleTypeProperty);
            var path = $"/{complexTypePropertyName}/{complexTypePropertyName}/{complexTypePropertyName}/{simpleTypePropertyName}";
            const int value = 42;

            // act
            PropertiesPathfinder.SetValue(model, path, value);

            // assert
            Assert.NotNull(model.ComplexTypeProperty?.ComplexTypeProperty?.ComplexTypeProperty);
        }

        [Fact]
        public void CanSetNestedSimpleTypeProperty()
        {
            // arrange
            var model = new ComplexPropertiesModel { ComplexTypeProperty = new ComplexPropertiesModel { ComplexTypeProperty = new ComplexPropertiesModel() } };
            const int value = 14;
            var complexTypePropertyName = nameof(ComplexPropertiesModel.ComplexTypeProperty);
            var simpleTypePropertyName = nameof(ComplexPropertiesModel.SimpleTypeProperty);
            var path = $"/{complexTypePropertyName}/{complexTypePropertyName}/{complexTypePropertyName}/{simpleTypePropertyName}";

            // act
            PropertiesPathfinder.SetValue(model, path, value);

            // assert
            Assert.Equal(value, model.ComplexTypeProperty.ComplexTypeProperty.ComplexTypeProperty.SimpleTypeProperty);
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
                ComplexTypeArrayProperty = new ComplexPropertiesModel[] 
                {
                    new ComplexPropertiesModel(),
                    new ComplexPropertiesModel(),
                    new ComplexPropertiesModel()
                }
            };

            const int changedValueIndex = 1;
            const int newValue = 531;
            var path = $"{nameof(ComplexPropertiesModel.ComplexTypeArrayProperty)}/{changedValueIndex}/{nameof(ComplexPropertiesModel.SimpleTypeProperty)}";

            // act
            PropertiesPathfinder.SetValue(model, path, newValue);

            // assert
            Assert.Equal(newValue, model.ComplexTypeArrayProperty[changedValueIndex].SimpleTypeProperty);
        }
    }
}
