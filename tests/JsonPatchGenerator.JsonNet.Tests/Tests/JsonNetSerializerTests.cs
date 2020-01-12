using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Json.NET.Serializer.Service;
using System.Collections.Generic;
using Xunit;

namespace JsonPatchGenerator.JsonNet.Tests.Tests
{
    public class JsonNetSerializerTests
    {
        [Fact]
        public void CanDeserialize() =>
            TestDeserialization((_, actual) => Assert.NotNull(actual));

        [Fact]
        public void DeserializationResultHasCorrectValue() =>
            TestDeserialization((expected, actual) => Assert.Equal(expected, actual));

        private delegate void DeserializationAssert(DiffDocument expected, DiffDocument actual);
        private void TestDeserialization(DeserializationAssert assert)
        {
            // arrange
            const string json = "[" +
                "{ \"op\": \"copy\", \"from\": \"/biscuits/0\", \"path\":\"/best_biscuit\" }" +
                "{ \"op\": \"add\", \"path\": \"/arr/1\", \"value\": 17 }" +
            "]";
            var expectedOperations = new List<Operation> {
                new Operation(OperationType.Copy, null, "/best_biscuit", "/biscuits/0"),
                new Operation(OperationType.Add, "str", "/arr/1")
            };

            var expected = new DiffDocument(expectedOperations);
            var target = new JsonNetSerializer();

            // act
            var result = target.Deserialize(json);

            // assert
            assert(expected, result);
        }

        [Fact]
        public void CanSerialize() =>
            TestSerialization((_, result) => Assert.NotEmpty(result));

        [Fact]
        public void SerializationResultCanBeDeserialized() =>
            TestSerialization((_, result) =>
            {
                var deserialized = new JsonNetSerializer().Deserialize(result);
                Assert.NotNull(deserialized);
            });

        [Fact]
        public void SerializationResultHasCorrectValueAfterDeserialization() =>
            TestSerialization((expected, result) =>
            {
                var deserialized = new JsonNetSerializer().Deserialize(result);
                Assert.Equal(expected, deserialized);
            });

        private delegate void SerializationAssert(DiffDocument document, string json);

        private void TestSerialization(SerializationAssert assert)
        {
            // arrange
            var operations = new List<Operation>
            {
                new Operation(OperationType.Test, 41, "/root/sub"),
                new Operation(OperationType.Add, 42, "/root/sub"),
                new Operation(OperationType.Copy, null, "/root/arr/7", "/root/arr/6"),
                new Operation(OperationType.Move, null, "/root/arr/1", "/root/arr/2"),
                new Operation(OperationType.Remove, null, "/root/arr/3"),
                new Operation(OperationType.Replace, "someVal", "/root/subb")
            };

            var model = new DiffDocument(operations);
            var target = new JsonNetSerializer();

            // act
            var result = target.Serialize(model);

            // assert
            assert(model, result);
        }
    }
}
