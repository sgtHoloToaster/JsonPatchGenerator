using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Core.Services;
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
    }
}
