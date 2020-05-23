using JsonPatchGenerator.Core.Services;
using Xunit;
using OneType;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    using static BaseTestsHelper;

    public class MoveCoreIntegrationTests : IMoveTests
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

        [Fact]
        public void SimpleTypeListMoveDoesntProduceExtraOperations() =>
            _base.SimpleTypeListMoveDoesntProduceExtraOperations();

        [Fact]
        public void SimpleTypeListMoveOperationHasCorrectFrom() =>
            _base.SimpleTypeListMoveOperationHasCorrectFrom();

        [Fact]
        public void SimpleTypeListMoveOperationHasCorrectPath() =>
            _base.SimpleTypeListMoveOperationHasCorrectPath();

        [Fact]
        public void SimpleTypeListMoveOperationHasCorrectValue() =>
            _base.SimpleTypeListMoveOperationHasCorrectValue();

        [Fact]
        public void SupportSimpleTypeListMoveOperation() =>
            _base.SupportSimpleTypeListMoveOperation();
    }
}
