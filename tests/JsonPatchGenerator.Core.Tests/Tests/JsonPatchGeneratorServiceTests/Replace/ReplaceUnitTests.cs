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

    public class ReplaceUnitTests : IReplaceTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();
        readonly ReplaceTestsBase _base;

        public ReplaceUnitTests()
        {
            MockCommonDependencies(_mocker);
            var operations = new List<Operation>();
            _mocker.GetMock<IPatchDocumentBuilder<IPatchDocument>>()
                .Setup(m => m.AppendReplaceOperation(It.IsAny<string>(), It.IsAny<object>()))
                .Callback<string, object>((path, value) => operations.Add(new Operation(OperationType.Replace, path, value)));
            _mocker.GetMock<IPatchDocumentBuilder<IPatchDocument>>()
                .Setup(m => m.Build())
                .Returns(new PatchDocument(operations));
            _base = new ReplaceTestsBase(GetTarget);
        }

        private JsonPatchGeneratorService GetTarget() =>
            _mocker.Create<JsonPatchGeneratorService>();

        [Fact]
        public void ArrayElementPropertyReplaceOperationHasCorrectPath() =>
            _base.ArrayElementPropertyReplaceOperationHasCorrectPath();

        [Fact]
        public void ArrayElementPropertyReplaceOperationHasCorrectValue() =>
            _base.ArrayElementPropertyReplaceOperationHasCorrectValue();

        [Fact]
        public void ArrayElementReplaceOperationHasCorrectPath() =>
            _base.ArrayElementReplaceOperationHasCorrectPath();

        [Fact]
        public void CanHandleMultipleDifferences() =>
            _base.CanHandleMultipleDifferences();

        [Fact]
        public void CreateCorrectPathForNestedPropertiesOnReplace() =>
            _base.CreateCorrectPathForNestedPropertiesOnReplace();

        [Fact]
        public void DontCreateExtraOperationsOnReplace() =>
            _base.DontCreateExtraOperationsOnReplace();

        [Fact]
        public void ReplaceOperationForComplexPropertiesHasCorrectValue() =>
            _base.ReplaceOperationForComplexPropertiesHasCorrectValue();

        [Fact]
        public void ReplaceOperationHasCorrectPath() =>
            _base.ReplaceOperationHasCorrectPath();

        [Fact]
        public void ReplaceOperationHasCorrectValue() =>
            _base.ReplaceOperationHasCorrectValue();

        [Fact]
        public void SimpleTypeArrayElementReplaceOperationHasCorrectValue() =>
            _base.SimpleTypeArrayElementReplaceOperationHasCorrectValue();

        [Fact]
        public void SupportReplaceOperationForSimpleTypes() =>
            _base.SupportReplaceOperationForSimpleTypes();

        [Fact]
        public void SupportReplaceOperationsForNestedObjects() =>
            _base.SupportReplaceOperationsForNestedObjects();

        [Fact]
        public void SupportReplacingPropertiesOfArrayElement() =>
            _base.SupportReplacingPropertiesOfArrayElement();

        [Fact]
        public void SupportReplacingValuesWithNull() =>
            _base.SupportReplacingValuesWithNull();

        [Fact]
        public void SupportSimpleTypeArrayElementReplacing() =>
            _base.SupportSimpleTypeArrayElementReplacing();

        [Fact]
        public void SupportStringElementReplacing() =>
            _base.SupportStringElementReplacing();

        [Fact]
        public void StringElementReplaceOperationHasCorrectPath() =>
            _base.StringElementReplaceOperationHasCorrectPath();

        [Fact]
        public void StringElementReplaceOperationHasCorrectValue() =>
            _base.StringElementReplaceOperationHasCorrectValue();

        [Fact]
        public void SupportSimpleTypeListElementReplacing() =>
            _base.SupportSimpleTypeListElementReplacing();

        [Fact]
        public void SimpleTypeListElementReplaceOperationHasCorrectPath() =>
            _base.SimpleTypeListElementReplaceOperationHasCorrectPath();

        [Fact]
        public void SimpleTypeListElementReplaceOperationHasCorrectValue() =>
            _base.SimpleTypeListElementReplaceOperationHasCorrectValue();

        [Fact]
        public void SupportReplacingPropertiesOfListElement() =>
            _base.SupportReplacingPropertiesOfListElement();

        [Fact]
        public void ListElementPropertyReplaceOperationHasCorrectPath() =>
            _base.ListElementPropertyReplaceOperationHasCorrectPath();

        [Fact]
        public void ListElementPropertyReplaceOperationHasCorrectValue() =>
            _base.ListElementPropertyReplaceOperationHasCorrectValue();
    }
}
