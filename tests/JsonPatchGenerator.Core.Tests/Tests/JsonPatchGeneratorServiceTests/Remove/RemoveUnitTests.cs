using AutoMoqSlim;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Enums;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    using static UnitTestsHelper;

    public class RemoveUnitTests : IRemoveTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();
        readonly RemoveTestsBase _base;

        public RemoveUnitTests()
        {
            MockCommonDependencies(_mocker);        
            var operations = new List<Operation>();
            _mocker.GetMock<IPatchDocumentBuilder<IPatchDocument>>()
                .Setup(m => m.AppendRemoveOperation(It.IsAny<string>()))
                .Callback<string>(path => operations.Add(new Operation(OperationType.Remove, path)));
            _mocker.GetMock<IPatchDocumentBuilder<IPatchDocument>>()
                .Setup(m => m.Build())
                .Returns(new PatchDocument(operations));
            _base = new RemoveTestsBase(GetTarget);
        }

        private JsonPatchGeneratorService GetTarget() =>
            _mocker.Create<JsonPatchGeneratorService>();

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

        [Fact]
        public void SimpleTypeListMoveDoesntProduceExtraOperations() =>
            _base.SimpleTypeListMoveDoesntProduceExtraOperations();

        [Fact]
        public void SimpleTypeListRemoveOperationHasCorrectPath() =>
            _base.SimpleTypeListRemoveOperationHasCorrectPath();

        [Fact]
        public void SimpleTypeListRemoveOperationHasCorrectValue() =>
            _base.SimpleTypeListRemoveOperationHasCorrectValue();

        [Fact]
        public void SupportSimpleTypeListRemoveOperation() =>
            _base.SupportSimpleTypeListRemoveOperation();
    }
}
