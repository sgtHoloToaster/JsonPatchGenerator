using AutoMoqCore;
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

    public class AddUnitTests : IAddTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();
        readonly AddTestsBase _base;

        public AddUnitTests()
        {
            MockCommonDependencies(_mocker);
            var operations = new List<Operation>();
            _mocker.GetMock<IPatchDocumentBuilder<IPatchDocument>>()
                .Setup(m => m.AppendAddOperation(It.IsAny<string>(), It.IsAny<object>()))
                .Callback<string, object>((path, value) => operations.Add(new Operation(OperationType.Add, path, value)));
            _mocker.GetMock<IPatchDocumentBuilder<IPatchDocument>>()
                .Setup(m => m.Build())
                .Returns(new PatchDocument(operations));
            _base = new AddTestsBase(GetTarget);
        }

        private JsonPatchGeneratorService GetTarget() =>
            _mocker.Create<JsonPatchGeneratorService>();

        [Fact]
        public void SimpleTypeArrayAddOperationHasCorrectPath() =>
            _base.SimpleTypeArrayAddOperationHasCorrectPath();

        [Fact]
        public void SimpleTypeArrayAddOperationHasCorrectValue() =>
            _base.SimpleTypeArrayAddOperationHasCorrectValue();

        [Fact]
        public void SimpleTypeArrayIndexBasedAddDoesntProduceExtraOperations() =>
            _base.SimpleTypeArrayIndexBasedAddDoesntProduceExtraOperations();

        [Fact]
        public void SimpleTypeArrayIndexBasedAddOperationHasCorrectPath() =>
            _base.SimpleTypeArrayIndexBasedAddOperationHasCorrectPath();

        [Fact]
        public void SimpleTypeArrayIndexBasedAddOperationHasCorrectValue() =>
            _base.SimpleTypeArrayIndexBasedAddOperationHasCorrectValue();

        [Fact]
        public void SupportSimpleTypeArrayAddOperation() =>
            _base.SupportSimpleTypeArrayAddOperation();

        [Fact]
        public void SupportSimpleTypeArrayIndexBasedAddOperation() =>
            _base.SupportSimpleTypeArrayIndexBasedAddOperation();

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
