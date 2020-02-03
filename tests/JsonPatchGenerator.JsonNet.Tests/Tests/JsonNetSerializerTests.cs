using AutoMoqCore;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Json.NET.Serializer.Service;
using JsonPatchGenerator.Interface.Enums;
using System.Collections.Generic;
using Xunit;
using JsonPatchGenerator.Interface.Services;
using Moq;

namespace JsonPatchGenerator.JsonNet.Tests.Tests
{
    public class JsonNetSerializerTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();
        public JsonNetSerializerTests()
        {
            var operations = new List<Operation>();
            _mocker.GetMock<IPatchDocumentBuilder>()
                .Setup(m => m.AppendOperation(It.IsAny<OperationType>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()))
                .Callback<OperationType, string, object, string>((type, path, value, from) => operations.Add(new Operation(type, path, value, from)));
            _mocker.GetMock<IPatchDocumentBuilder>()
                .Setup(m => m.Build())
                .Returns(new PatchDocument(operations));
            _mocker.GetMock<IPatchDocumentBuilderFactory>()
                .Setup(m => m.Create())
                .Returns(() => _mocker.Create<IPatchDocumentBuilder>());
        }

        private JsonNetSerializer GetTarget() =>
            _mocker.Create<JsonNetSerializer>();

        [Fact]
        public void CanDeserialize() =>
            TestDeserialization((_, actual) => Assert.NotNull(actual));

        [Fact]
        public void DeserializationResultHasCorrectValue() =>
            TestDeserialization((expected, actual) => Assert.Equal(expected, actual));

        private delegate void DeserializationAssert(IPatchDocument expected, IPatchDocument actual);
        private void TestDeserialization(DeserializationAssert assert)
        {
            // arrange
            const string json = "[" +
                "{ \"op\": \"copy\", \"from\": \"/biscuits/0\", \"path\":\"/best_biscuit\" }," +
                "{ \"op\": \"add\", \"path\": \"/arr/1\", \"value\": \"str\" }," +
                "{ \"op\": \"replace\", \"path\": \"/nullableProperty\", \"value\": null }" +
            "]";
            var expectedOperations = new List<Operation> {
                new Operation(OperationType.Copy, "/best_biscuit", null, "/biscuits/0"),
                new Operation(OperationType.Add, "/arr/1", "str"),
                new Operation(OperationType.Replace, "/nullableProperty", null)
            };

            var expected = new PatchDocument(expectedOperations);
            var target = GetTarget();

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
                var target = GetTarget();
                var deserialized = target.Deserialize(result);
                Assert.NotNull(deserialized);
            });

        [Fact]
        public void SerializationResultHasCorrectValueAfterDeserialization() =>
            TestSerialization((expected, result) =>
            {
                var target = GetTarget();
                var deserialized = target.Deserialize(result);
                Assert.Equal(expected, deserialized);
            });

        private delegate void SerializationAssert(IPatchDocument document, string json);

        private void TestSerialization(SerializationAssert assert)
        {
            // arrange
            var operations = new List<Operation>
            {
                new Operation(OperationType.Test, "/root/sub", (long)41),
                new Operation(OperationType.Add, "/root/sub", (long)42),
                new Operation(OperationType.Copy, "/root/arr/7", null, "/root/arr/6"),
                new Operation(OperationType.Move, "/root/arr/1", null, "/root/arr/2"),
                new Operation(OperationType.Remove, "/root/arr/3"),
                new Operation(OperationType.Replace, "/root/subb", "someVal")
            };

            var model = new PatchDocument(operations);
            var target = GetTarget();

            // act
            var result = target.Serialize(model);

            // assert
            assert(model, result);
        }
    }
}
