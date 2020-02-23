namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public interface IMoveTests
    {
        void SimpleTypeArrayMoveDoesntProduceExtraOperations();
        void SimpleTypeArrayMoveOperationHasCorrectFrom();
        void SimpleTypeArrayMoveOperationHasCorrectPath();
        void SimpleTypeArrayMoveOperationHasCorrectValue();
        void SupportSimpleTypeArrayMoveOperation();
        void SimpleTypeListMoveDoesntProduceExtraOperations();
        void SimpleTypeListMoveOperationHasCorrectFrom();
        void SimpleTypeListMoveOperationHasCorrectPath();
        void SimpleTypeListMoveOperationHasCorrectValue();
        void SupportSimpleTypeListMoveOperation();
    }
}