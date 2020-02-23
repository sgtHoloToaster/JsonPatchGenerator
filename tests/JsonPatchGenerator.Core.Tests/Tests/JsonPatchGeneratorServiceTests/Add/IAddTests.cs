namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public interface IAddTests
    {
        void SimpleTypeArrayAddOperationHasCorrectPath();
        void SimpleTypeArrayAddOperationHasCorrectValue();
        void SimpleTypeArrayIndexBasedAddDoesntProduceExtraOperations();
        void SimpleTypeArrayIndexBasedAddOperationHasCorrectPath();
        void SimpleTypeArrayIndexBasedAddOperationHasCorrectValue();
        void SupportSimpleTypeArrayAddOperation();
        void SupportSimpleTypeArrayIndexBasedAddOperation();
        void SupportSimpleTypeListAddOperation();
        void SimpleTypeListAddOperationHasCorrectValue();
        void SimpleTypeListAddOperationHasCorrectPath();
    }
}