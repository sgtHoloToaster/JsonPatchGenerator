using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Enums;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class DefaultPatchDocumentBuilderTests
    {
        [Fact]
        public void CanBuildDocument() =>
            TestBuild(Assert.NotNull);

        [Fact]
        public void BuiltDocumentHasCorrectType() =>
            TestBuild(result => Assert.True(result is PatchDocument));

        private void TestBuild(Action<IPatchDocument> assert)
        {
            // arrange
            var target = new DefaultPatchDocumentBuilder();

            // act
            var result = target.Build();

            // assert
            assert(result);
        }

        [Fact]
        public void AddOperationHasCorrectType() =>
            TestAddOperation(HasCorrectType);

        [Fact]
        public void AddOperationHasCorrectValue() =>
            TestAddOperation(HasCorrectValue);

        [Fact]
        public void AddOperationHasCorrectPath() =>
            TestAddOperation(HasCorrectPath);

        private void TestAddOperation(Action<Operation, Operation> assert) =>
            TestOperation(
                new Operation(OperationType.Add, "/somePath", 42),
                (builder, expectedOperation) => builder.AppendAddOperation(expectedOperation.Path, expectedOperation.Value),
                assert);

        [Fact]
        public void ReplaceOperationHasCorrectType() =>
            TestReplaceOperation(HasCorrectType);

        [Fact]
        public void ReplaceOperationHasCorrectValue() =>
            TestReplaceOperation(HasCorrectValue);

        [Fact]
        public void ReplaceOperationHasCorrectPath() =>
            TestReplaceOperation(HasCorrectPath);

        private void TestReplaceOperation(Action<Operation, Operation> assert) =>
            TestOperation(
                new Operation(OperationType.Replace, "/someReplacePath", 113),
                (builder, expectedOperation) => builder.AppendReplaceOperation(expectedOperation.Path, expectedOperation.Value),
                assert);

        [Fact]
        public void TestOperationHasCorrectType() =>
            TestTestOperation(HasCorrectType);

        [Fact]
        public void TestOperationHasCorrectValue() =>
            TestTestOperation(HasCorrectValue);

        [Fact]
        public void TestOperationHasCorrectPath() =>
            TestTestOperation(HasCorrectPath);

        private void TestTestOperation(Action<Operation, Operation> assert) =>
            TestOperation(
                new Operation(OperationType.Test, "/someTestPath", "test"),
                (builder, expectedOperation) => builder.AppendTestOperation(expectedOperation.Path, expectedOperation.Value),
                assert);

        [Fact]
        public void RemoveOperationHasCorrectType() =>
            TestRemoveOperation(HasCorrectType);

        [Fact]
        public void RemoveOperationHasCorrectPath() =>
            TestRemoveOperation(HasCorrectPath);

        private void TestRemoveOperation(Action<Operation, Operation> assert) =>
            TestOperation(
                new Operation(OperationType.Remove, "/someRemovePath/1"),
                (builder, expectedOperation) => builder.AppendRemoveOperation(expectedOperation.Path),
                assert);

        [Fact]
        public void MoveOperationHasCorrectType() =>
            TestMoveOperation(HasCorrectType);

        [Fact]
        public void MoveOperationHasCorrectPath() =>
            TestMoveOperation(HasCorrectPath);

        [Fact]
        public void MoveOperationHasCorrectFrom() =>
            TestMoveOperation(HasCorrectFrom);

        private void TestMoveOperation(Action<Operation, Operation> assert) =>
            TestOperation(
                new Operation(OperationType.Move, "/someMovePath", null, "/someMoveFrom/from"),
                (builder, expectedOperation) => builder.AppendMoveOperation(expectedOperation.Path, expectedOperation.From),
                assert);

        [Fact]
        public void CopyOperationHasCorrectType() =>
            TestCopyOperation(HasCorrectType);

        [Fact]
        public void CopyOperationHasCorrectPath() =>
            TestCopyOperation(HasCorrectPath);

        [Fact]
        public void CopyOperationHasCorrectFrom() =>
            TestCopyOperation(HasCorrectFrom);

        private void TestCopyOperation(Action<Operation, Operation> assert) =>
            TestOperation(
                new Operation(OperationType.Copy, "/someCopyPath/3", null, "/someAnother/copyPath/1"),
                (builder, expectedOperation) => builder.AppendCopyOperation(expectedOperation.Path, expectedOperation.From),
                assert);

        private void TestOperation(Operation expectedOperation, Action<DefaultPatchDocumentBuilder, Operation> testMethod, Action<Operation, Operation> assert) =>
            TestOperation(
                builder => testMethod(builder, expectedOperation),
                result => assert(expectedOperation, result));

        private void HasCorrectType(Operation expected, Operation result) =>
            Assert.Equal(expected.Type, result.Type);

        private void HasCorrectValue(Operation expected, Operation result) =>
            Assert.Equal(expected.Value, result.Value);

        private void HasCorrectPath(Operation expected, Operation result) =>
            Assert.Equal(expected.Path, result.Path);

        private void HasCorrectFrom(Operation expected, Operation result) =>
            Assert.Equal(expected.Path, result.Path);

        private void TestOperation(Action<DefaultPatchDocumentBuilder> appendOperationAction, Action<Operation> assert)
        {
            // arrange
            var target = new DefaultPatchDocumentBuilder();

            // act
            appendOperationAction(target);
            var result = target.Build();

            // assert
            assert(result.Operations.FirstOrDefault());
        }

        [Theory]
        [MemberData(nameof(GetTestOperations))]
        public void CustomTypeOperationHasCorrectType(Operation operation) =>
            TestCustomOperation(operation, HasCorrectType);

        private void TestCustomOperation(Operation expectedOperation, Action<Operation, Operation> assert) =>
            TestOperation(
                expectedOperation, 
                (builder, expected) => builder.AppendOperation(expected.Type, expected.Path, expected.Value, expected.From),
                assert);

        public static IEnumerable<object[]> GetTestOperations()
        {
            yield return new object[] { new Operation(OperationType.Add, "/someProperty/1", 42) };
            yield return new object[] { new Operation(OperationType.Remove, "/unusedProp") };
            yield return new object[] { new Operation(OperationType.Test, "/need/To/Test", "faaaaa") };
            yield return new object[] { new Operation(OperationType.Move, "/fromHere", null, "/there") };
            yield return new object[] { new Operation(OperationType.Replace, "/objProp", new object()) };
        }
    }
}
