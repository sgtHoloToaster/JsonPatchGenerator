using JsonPatchGenerator.Core.Services;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public class RemoveCoreIntegrationTests : IRemoveTests
    {
        readonly RemoveTestsBase _base;

        public RemoveCoreIntegrationTests()
        {
            _base = new RemoveTestsBase(GetTarget);
        }

        private JsonPatchGeneratorService GetTarget() =>
            new JsonPatchGeneratorService(new DefaultTypeResolver(), new DefaultPatchDocumentBuilderFactory());

        [Fact]
        public void SimpleTypeArrayMoveDoesntProduceExtraOperations() =>
            _base.SimpleTypeArrayMoveDoesntProduceExtraOperations();

        [Fact]
        public void SimpleTypeArrayRemoveOperationHasCorrectPath() =>
            _base.SimpleTypeArrayRemoveOperationHasCorrectPath();

        [Fact]
        public void SimpleTypeArrayRemoveOperationHasCorrectValue() =>
            _base.SimpleTypeArrayRemoveOperationHasCorrectValue();

        [Fact]
        public void SupportSimpleTypeArrayRemoveOperation() =>
            _base.SupportSimpleTypeArrayRemoveOperation();
    }
}
