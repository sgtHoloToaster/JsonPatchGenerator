namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public interface IMoveTests
    {
        void SimpleTypeArrayMoveDoesntProduceExtraOperations();
        void SimpleTypeArrayMoveOperationHasCorrectFrom();
        void SimpleTypeArrayMoveOperationHasCorrectPath();
        void SimpleTypeArrayMoveOperationHasCorrectValue();
        void SupportSimpleTypeArrayMoveOperation();
    }
}