using AutoMoqCore;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.JsonNet.Enums;
using System.Linq;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class JsonPatchGeneratorTests
    {
        protected readonly AutoMoqer Mocker = new AutoMoqer();
        protected delegate void AssertAction(IPatchDocument result, string path, object newValue);
        protected delegate void MoveAssertAction(IPatchDocument result, string path, object value, string from);

        public JsonPatchGeneratorTests()
        {
            Mocker.SetInstance<ITypeResolver>(new DefaultTypeResolver());
        }

        protected void HasCorrectValue(IPatchDocument result, string path, object expectedValue, string from) =>
            HasCorrectValue(result, expectedValue);

        protected void HasCorrectValue(IPatchDocument result, string path, object expectedValue) =>
            HasCorrectValue(result, expectedValue);

        protected void HasCorrectValue(IPatchDocument result, object expectedValue)
        {
            Assert.NotNull(result?.Operations);
            var operation = result.Operations.First();
            Assert.Equal(expectedValue, operation.Value);
        }

        protected void HasOperation(IPatchDocument result, OperationType operationType)
        {
            Assert.NotNull(result?.Operations);
            Assert.NotEmpty(result.Operations);
            Assert.Contains(result.Operations, o => o.Type == operationType);
        }

        protected void HasNoExtraOperations(IPatchDocument result, string path, object newValue, string from) =>
            HasNoExtraOperations(result);

        protected void HasNoExtraOperations(IPatchDocument result, string path, object newValue) =>
            HasNoExtraOperations(result);

        protected void HasNoExtraOperations(IPatchDocument result)
        {
            Assert.NotNull(result?.Operations);
            Assert.Single(result.Operations);
        }

        protected void HasCorrectPath(IPatchDocument result, string path)
        {
            Assert.NotNull(result?.Operations);
            var operation = result.Operations.First();
            Assert.Equal(path, operation.Path);
        }

        protected void HasCorrectPath(IPatchDocument result, string path, object newValue) =>
            HasCorrectPath(result, path);

        protected void HasCorrectPath(IPatchDocument result, string path, object newValue, string from) =>
            HasCorrectPath(result, path);

        protected void HasCorrectFrom(IPatchDocument result, string from)
        {
            Assert.NotNull(result?.Operations);
            var operation = result.Operations.First();
            Assert.Equal(from, operation.From);
        }

        protected void HasCorrectFrom(IPatchDocument result, string path, object newValue, string from) =>
            HasCorrectFrom(result, from);
    }
}
