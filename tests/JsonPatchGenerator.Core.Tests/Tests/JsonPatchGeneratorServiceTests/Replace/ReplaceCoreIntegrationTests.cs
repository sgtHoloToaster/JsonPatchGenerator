using JsonPatchGenerator.Core.Services;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public class ReplaceCoreIntegrationTests : IReplaceTests
    {
        readonly ReplaceTestsBase _base;

        public ReplaceCoreIntegrationTests()
        {
            _base = new ReplaceTestsBase(GetTarget);
        }

        private JsonPatchGeneratorService GetTarget() =>
            new JsonPatchGeneratorService(new DefaultTypeResolver(), new DefaultPatchDocumentBuilderFactory());

        [Fact]
        public void ArrayElementPropertyReplaceOperationHasCorrectPath() =>
            _base.ArrayElementPropertyReplaceOperationHasCorrectPath();

        [Fact]
        public void ArrayElementPropertyReplaceOperationHasCorrectValue() =>
            _base.ArrayElementPropertyReplaceOperationHasCorrectValue();

        [Fact]
        public void ArrayElementReplaceOperationHasCorrectPath() =>
            _base.ArrayElementReplaceOperationHasCorrectPath();

        [Fact]
        public void CanHandleMultipleDifferences() =>
            _base.CanHandleMultipleDifferences();

        [Fact]
        public void CreateCorrectPathForNestedPropertiesOnReplace() =>
            _base.CreateCorrectPathForNestedPropertiesOnReplace();

        [Fact]
        public void DontCreateExtraOperationsOnReplace() =>
            _base.DontCreateExtraOperationsOnReplace();

        [Fact]
        public void ReplaceOperationForComplexPropertiesHasCorrectValue() =>
            _base.ReplaceOperationForComplexPropertiesHasCorrectValue();

        [Fact]
        public void ReplaceOperationHasCorrectPath() =>
            _base.ReplaceOperationHasCorrectPath();

        [Fact]
        public void ReplaceOperationHasCorrectValue() =>
            _base.ReplaceOperationHasCorrectValue();

        [Fact]
        public void SimpleTypeArrayElementReplaceOperationHasCorrectValue() =>
            _base.SimpleTypeArrayElementReplaceOperationHasCorrectValue();

        [Fact]
        public void SupportReplaceOperationForSimpleTypes() =>
            _base.SupportReplaceOperationForSimpleTypes();

        [Fact]
        public void SupportReplaceOperationsForNestedObjects() =>
            _base.SupportReplaceOperationsForNestedObjects();

        [Fact]
        public void SupportReplacingPropertiesOfArrayElement() =>
            _base.SupportReplacingPropertiesOfArrayElement();

        [Fact]
        public void SupportReplacingValuesWithNull() =>
            _base.SupportReplacingValuesWithNull();

        [Fact]
        public void SupportSimpleTypeArrayElementReplacing() =>
            _base.SupportSimpleTypeArrayElementReplacing();

        [Fact]
        public void SupportStringElementReplacing() =>
            _base.SupportStringElementReplacing();

        [Fact]
        public void StringElementReplaceOperationHasCorrectPath() =>
            _base.StringElementReplaceOperationHasCorrectPath();

        [Fact]
        public void StringElementReplaceOperationHasCorrectValue() =>
            _base.StringElementReplaceOperationHasCorrectValue();
    }
}
