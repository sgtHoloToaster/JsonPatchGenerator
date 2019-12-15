using AutoMoqCore;
using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Services;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class JsonPatchGeneratorTests
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
    }
}
