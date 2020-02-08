using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Enums;
using System.Linq;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public class BaseTestsHelper
    {
        public delegate void AssertAction(IPatchDocument result, string path, object newValue);
        public delegate void MoveAssertAction(IPatchDocument result, string path, object value, string from);

        public static void HasCorrectValue(IPatchDocument result, string path, object expectedValue, string from) =>
            HasCorrectValue(result, expectedValue);

        public static void HasCorrectValue(IPatchDocument result, string path, object expectedValue) =>
            HasCorrectValue(result, expectedValue);

        public static void HasCorrectValue(IPatchDocument result, object expectedValue)
        {
            Assert.NotNull(result?.Operations);
            var operation = result.Operations.First();
            Assert.Equal(expectedValue, operation.Value);
        }

        public static void HasOperation(IPatchDocument result, OperationType operationType)
        {
            Assert.NotNull(result?.Operations);
            Assert.NotEmpty(result.Operations);
            Assert.Contains(result.Operations, o => o.Type == operationType);
        }

        public static void HasNoExtraOperations(IPatchDocument result, string path, object newValue, string from) =>
            HasNoExtraOperations(result);

        public static void HasNoExtraOperations(IPatchDocument result, string path, object newValue) =>
            HasNoExtraOperations(result);

        public static void HasNoExtraOperations(IPatchDocument result)
        {
            Assert.NotNull(result?.Operations);
            Assert.Single(result.Operations);
        }

        public static void HasCorrectPath(IPatchDocument result, string path)
        {
            Assert.NotNull(result?.Operations);
            var operation = result.Operations.First();
            Assert.Equal(path, operation.Path);
        }

        public static void HasCorrectPath(IPatchDocument result, string path, object newValue) =>
            HasCorrectPath(result, path);

        public static void HasCorrectPath(IPatchDocument result, string path, object newValue, string from) =>
            HasCorrectPath(result, path);

        public static void HasCorrectFrom(IPatchDocument result, string from)
        {
            Assert.NotNull(result?.Operations);
            var operation = result.Operations.First();
            Assert.Equal(from, operation.From);
        }

        public static void HasCorrectFrom(IPatchDocument result, string path, object newValue, string from) =>
            HasCorrectFrom(result, from);
    }
}
