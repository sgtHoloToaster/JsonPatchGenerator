using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Helpers;
using JsonPatchGenerator.Core.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class DefaultTypeResolverTests
    {
        [Fact]
        public void ReturnAllSimpleTypesProperties() =>
            DoTest<SimpleTypesPublicPropertiesModel>();

        [Fact]
        public void ReturnOnlyPublicProperties() =>
            DoTest<DifferentAccessLevelPropertiesModel>();

        [Fact]
        public void ReturnComplexTypesProperties() =>
            DoTest<ComplexTypesPublicPropertiesModel>();

        private void DoTest<T>() where T : new()
        {
            // arrange
            var target = new DefaultTypeResolver();
            var expected = GetPublicProperties(typeof(T));

            // act
            var result = target.GetProperties(typeof(T));

            // assert
            AssertResult(expected, result);
        }

        [Fact]
        public void ReturnDerivedProperties()
        {
            // arrange
            var baseType = typeof(SimpleTypesPublicPropertiesModel);
            var derivedType = typeof(DerivedPropertiesModel);

            // assert
            Assert.True(baseType.IsAssignableFrom(derivedType));

            // arrange
            var target = new DefaultTypeResolver();
            var expected = GetPublicProperties(baseType);

            // act
            var result = target.GetProperties(derivedType);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(expected.All(p => result.Any(r => r.Equals(p))));
        }

        private IEnumerable<ObjectProperty> GetPublicProperties(Type type) =>
            type.GetProperties()
                .Select(p => new ObjectProperty(p))
                .ToList();

        private void AssertResult(IEnumerable<ObjectProperty> expected, IEnumerable<ObjectProperty> actual)
        {
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.OrderBy(p => p.Name), actual.OrderBy(p => p.Name));
        }

        [Fact]
        public void CanGetObjectValueByProperty()
        {
            // arrange
            var model = new SimpleTypesPublicPropertiesModel();
            var property = model.GetType().GetProperty(nameof(SimpleTypesPublicPropertiesModel.IntProperty));
            property.SetValue(model, 42);
            var target = new DefaultTypeResolver();
            var objectProperty = new ObjectProperty(property);

            // act
            var result = target.GetValue(model, objectProperty);

            // assert
            Assert.NotNull(result);
            Assert.Equal(property.PropertyType, result.GetType());
            Assert.Equal(property.GetValue(model), result);
        }

        [Fact]
        public void CanCalculateHashCode()
        {
            // arrange
            var model = new SimpleTypesPublicPropertiesModel();
            var target = new DefaultTypeResolver();

            // act & assert
            target.GetHashCode(model);
        }

        [Fact]
        public void ProduceTheSameHashCodeForEqualModels()
        {
            // arrange
            var first = new SimpleTypesPublicPropertiesModel
            {
                BoolProperty = true,
                FloatProperty = 10.4311f,
                CharProperty = 'c',
                StringProperty = "omg"
            };

            var second = ObjectCloner.DeepClone(first);
            var target = new DefaultTypeResolver();

            // act
            var firstHashCode = target.GetHashCode(first);
            var secondHashCode = target.GetHashCode(second);

            // assert
            Assert.Equal(firstHashCode, secondHashCode);
        }

        [Fact]
        public void ProduceDifferentHashCodeForNonEqualModels()
        {
            // arrange
            var first = new SimpleTypesPublicPropertiesModel
            {
                StringProperty = "Somebody once told me"
            };

            var second = new SimpleTypesPublicPropertiesModel
            {
                StringProperty = "the world is gonna roll me"
            };
            var target = new DefaultTypeResolver();

            // act
            var firstHashCode = target.GetHashCode(first);
            var secondHashCode = target.GetHashCode(second);

            // assert
            Assert.NotEqual(firstHashCode, secondHashCode);
        }

        [Fact]
        public void ProduceDifferentHashCodeForObjectsWithDifferentComplexProperties()
        {
            // arrange
            var first = new SimpleTypesPublicPropertiesModel
            {
                ObjectProperty = new SimpleTypesPublicPropertiesModel
                {
                    ObjectProperty = new SimpleTypesPublicPropertiesModel { BoolProperty = false }
                }
            };

            var second = new SimpleTypesPublicPropertiesModel
            {
                ObjectProperty = new SimpleTypesPublicPropertiesModel
                {
                    ObjectProperty = new SimpleTypesPublicPropertiesModel { BoolProperty = true }
                }
            };
            var target = new DefaultTypeResolver();

            // act
            var firstHashCode = target.GetHashCode(first);
            var secondHashCode = target.GetHashCode(second);

            // assert
            Assert.NotEqual(firstHashCode, secondHashCode);
        }

        [Fact]
        public void ProduceTheSameHashCodeForObjectsWithEqualComplexProperties()
        {
            // arrange
            var first = new SimpleTypesPublicPropertiesModel
            {
                ObjectProperty = new SimpleTypesPublicPropertiesModel
                {
                    ObjectProperty = new SimpleTypesPublicPropertiesModel { BoolProperty = true }
                }
            };

            var second = ObjectCloner.DeepClone(first);
            var target = new DefaultTypeResolver();

            // act
            var firstHashCode = target.GetHashCode(first);
            var secondHashCode = target.GetHashCode(second);

            // assert
            Assert.Equal(firstHashCode, secondHashCode);
        }
    }
}
