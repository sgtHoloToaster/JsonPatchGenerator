using JsonPatchGenerator.Core.Services;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    using static Helper;

    public class MoveCoreIntegrationTests
    {
        readonly MoveTestsBase _base;

        public MoveCoreIntegrationTests()
        {
            _base = new MoveTestsBase(GetTarget);
        }

        private JsonPatchGeneratorService GetTarget() =>
            new JsonPatchGeneratorService(new DefaultTypeResolver(), new DefaultPatchDocumentBuilderFactory());

        [Fact]
        public void SupportSimpleTypeArrayMoveOperation() =>
            _base.SupportSimpleTypeArrayMoveOperation();

        [Fact]
        public void SimpleTypeArrayMoveOperationHasCorrectPath() =>
            _base.SimpleTypeArrayMoveOperationHasCorrectPath();

        [Fact]
        public void SimpleTypeArrayMoveOperationHasCorrectValue() =>
            _base.SimpleTypeArrayMoveOperationHasCorrectValue();

        [Fact]
        public void SimpleTypeArrayMoveOperationHasCorrectFrom() =>
            _base.SimpleTypeArrayMoveOperationHasCorrectFrom();

        [Fact]
        public void SimpleTypeArrayMoveDoesntProduceExtraOperations() =>
            _base.SimpleTypeArrayMoveDoesntProduceExtraOperations();
    }
}
