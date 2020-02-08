using AutoMoqCore;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Enums;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public class MoveUnitTests : IMoveTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();
        readonly MoveTestsBase _base;

        public MoveUnitTests()
        {
            var operations = new List<Operation>();
            _mocker.GetMock<IPatchDocumentBuilder>()
                .Setup(m => m.AppendMoveOperation(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((path, from) => operations.Add(new Operation(OperationType.Move, path, null, from)));
            _mocker.GetMock<IPatchDocumentBuilder>()
                .Setup(m => m.Build())
                .Returns(new PatchDocument(operations));
            _mocker.GetMock<IPatchDocumentBuilderFactory>()
                .Setup(m => m.Create())
                .Returns(() => _mocker.Create<IPatchDocumentBuilder>());
            _base = new MoveTestsBase(GetTarget);
        }

        private JsonPatchGeneratorService GetTarget() =>
            _mocker.Create<JsonPatchGeneratorService>();

        [Fact]
        public void SimpleTypeArrayMoveDoesntProduceExtraOperations() =>
            _base.SimpleTypeArrayMoveDoesntProduceExtraOperations();

        [Fact]
        public void SimpleTypeArrayMoveOperationHasCorrectFrom() =>
            _base.SimpleTypeArrayMoveOperationHasCorrectFrom();

        [Fact]
        public void SimpleTypeArrayMoveOperationHasCorrectPath() =>
            _base.SimpleTypeArrayMoveOperationHasCorrectPath();

        [Fact]
        public void SimpleTypeArrayMoveOperationHasCorrectValue() =>
            _base.SimpleTypeArrayMoveOperationHasCorrectValue();

        [Fact]
        public void SupportSimpleTypeArrayMoveOperation() =>
            _base.SupportSimpleTypeArrayMoveOperation();
    }
}
