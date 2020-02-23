namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public interface IRemoveTests
    {
        void SimpleTypeArrayMoveDoesntProduceExtraOperations();
        void SimpleTypeArrayRemoveOperationHasCorrectPath();
        void SimpleTypeArrayRemoveOperationHasCorrectValue();
        void SupportSimpleTypeArrayRemoveOperation();
        void SimpleTypeListMoveDoesntProduceExtraOperations();
        void SimpleTypeListRemoveOperationHasCorrectPath();
        void SimpleTypeListRemoveOperationHasCorrectValue();
        void SupportSimpleTypeListRemoveOperation();
    }
}