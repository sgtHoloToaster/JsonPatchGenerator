using JsonPatchGenerator.Core.Services;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public class AddCoreIntegrationTests : IAddTests
    {
        readonly AddTestsBase _base;

        public AddCoreIntegrationTests()
        {
            _base = new AddTestsBase(GetTarget);
        }

        private JsonPatchGeneratorService GetTarget() =>
            new JsonPatchGeneratorService(new DefaultTypeResolver(), new DefaultPatchDocumentBuilderFactory());

        [Fact]
        public void SupportSimpleTypeArrayAddOperation() =>
            _base.SupportSimpleTypeArrayAddOperation();

        [Fact]
        public void SimpleTypeArrayAddOperationHasCorrectPath() =>
            _base.SimpleTypeArrayAddOperationHasCorrectPath();

        [Fact]
        public void SimpleTypeArrayAddOperationHasCorrectValue() =>
            _base.SimpleTypeArrayAddOperationHasCorrectValue();


        [Fact]
        public void SupportSimpleTypeArrayIndexBasedAddOperation() =>
            _base.SupportSimpleTypeArrayIndexBasedAddOperation();

        [Fact]
        public void SimpleTypeArrayIndexBasedAddDoesntProduceExtraOperations() =>
            _base.SimpleTypeArrayIndexBasedAddDoesntProduceExtraOperations();

        [Fact]
        public void SimpleTypeArrayIndexBasedAddOperationHasCorrectValue() =>
            _base.SimpleTypeArrayIndexBasedAddOperationHasCorrectValue();

        [Fact]
        public void SimpleTypeArrayIndexBasedAddOperationHasCorrectPath() =>
            _base.SimpleTypeArrayIndexBasedAddOperationHasCorrectPath();

        [Fact]
        public void SupportSimpleTypeListAddOperation() =>
            _base.SupportSimpleTypeListAddOperation();

        [Fact]
        public void SimpleTypeListAddOperationHasCorrectValue() =>
            _base.SimpleTypeListAddOperationHasCorrectValue();

        [Fact]
        public void SimpleTypeListAddOperationHasCorrectPath() =>
            _base.SimpleTypeListAddOperationHasCorrectPath();
    }
}
