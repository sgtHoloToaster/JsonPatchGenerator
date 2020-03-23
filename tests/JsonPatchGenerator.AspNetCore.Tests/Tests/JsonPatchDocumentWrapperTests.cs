using AutoMoqCore;
using JsonPatchGenerator.Core.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JsonPatchGenerator.AspNetCore.Tests.Tests
{
    public class JsonPatchDocumentWrapperTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();

        [Fact]
        public void ReturnsCorrectOperationsList()
        {
            // arrange
            var (expectedOperations, operations) = GetOperations();
            _mocker.GetMock<IJsonPatchDocument>()
                .Setup(m => m.GetOperations())
                .Returns(operations);
            var target = _mocker.Create<JsonPatchDocumentWrapper>();

            // act
            var result = target.Operations;

            // assert
            Assert.Equal(expectedOperations, result);
        }

        [Fact]
        public void ReturnsCorrectWrappedValue()
        {
            // arrange
            var (_, expectedOperations) = GetOperations();
            _mocker.GetMock<IJsonPatchDocument>()
                .Setup(m => m.GetOperations())
                .Returns(expectedOperations);
            var target = _mocker.Create<JsonPatchDocumentWrapper>();

            // act
            var result = target.GetValue();

            // assert
            Assert.Equal(expectedOperations, result.GetOperations());
        }

        private (List<Interface.Models.Operation>, List<Operation>) GetOperations()
        {
            var operations = new List<Interface.Models.Operation>
            {
                new Interface.Models.Operation(Interface.Enums.OperationType.Add, "/addPath", 33),
                new Interface.Models.Operation(Interface.Enums.OperationType.Copy, "/copyPath", null, "/copyFrom"),
                new Interface.Models.Operation(Interface.Enums.OperationType.Move, "/movePath", null, "/moveFrom"),
                new Interface.Models.Operation(Interface.Enums.OperationType.Remove, "/removePath"),
                new Interface.Models.Operation(Interface.Enums.OperationType.Replace, "/replacePath", 1),
                new Interface.Models.Operation(Interface.Enums.OperationType.Test, "/testPath", "value")
            };

            var expectedOperations = operations
                .Select(o => new Operation(EnumsHelper.GetEnumMemberAttributeValue(o.Type), o.Path, o.From, o.Value))
                .ToList();

            return (operations, expectedOperations);
        }
    }
}
