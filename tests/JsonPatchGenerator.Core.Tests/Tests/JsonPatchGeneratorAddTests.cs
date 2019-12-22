using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models;
using System;
using System.Linq;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class JsonPatchGeneratorAddTests : JsonPatchGeneratorTests
    {
        [Fact]
        public void SupportSimpleTypeArrayAddOperation() =>
            TestSimpleTypeArrayAddOperation((result, _, __) =>
            {
                Assert.NotNull(result);
                Assert.Contains(result.Operations, o => o.Type == OperationType.Add);
            });

        [Fact]
        public void SimpleTypeArrayAddOperationHasCorrectPath() =>
            TestSimpleTypeArrayAddOperation((result, _, expectedPath) =>
            {
                var operation = result.Operations.First();
                Assert.Equal(expectedPath, operation.Path);
            });

        [Fact]
        public void SimpleTypeArrayAddOperationHasCorrectValue() =>
            TestSimpleTypeArrayAddOperation((result, expectedValue, _) =>
            {
                var operation = result.Operations.First();
                Assert.Equal(expectedValue, operation.Value);
            });

        private void TestSimpleTypeArrayAddOperation(Action<DiffDocument, int, string> assert)
        {
            // arrange
            const int addedValue = 4;
            var first = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 2, 3 } };
            var second = new ComplexPropertiesModel { SimpleTypeArray = new[] { 1, 2, 3, addedValue } };
            var expectedPath = $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/-";

            var target = Mocker.Create<JsonPatchGeneratorService>();

            // act
            var result = target.GetDiff(first, second);

            // assert
            assert(result, addedValue, expectedPath);
        }
    }
}
