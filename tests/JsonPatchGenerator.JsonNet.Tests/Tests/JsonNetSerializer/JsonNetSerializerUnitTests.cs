using AutoMoqCore;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.JsonNet.Serializer.Service;
using JsonPatchGenerator.Interface.Enums;
using System.Collections.Generic;
using Xunit;
using JsonPatchGenerator.Interface.Services;
using Moq;

namespace JsonPatchGenerator.JsonNet.Tests.Tests
{
    public class JsonNetSerializerUnitTests : IJsonNetSerializerTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();
        readonly JsonNetSerializerTestsBase _testsBase;

        public JsonNetSerializerUnitTests()
        {
            _testsBase = new JsonNetSerializerTestsBase(GetTarget);
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
            _testsBase.CanDeserialize();

        [Fact]
        public void CanSerialize() =>
            _testsBase.CanSerialize();

        [Fact]
        public void DeserializationResultHasCorrectValue() =>
            _testsBase.DeserializationResultHasCorrectValue();

        [Fact]
        public void SerializationResultCanBeDeserialized() =>
            _testsBase.SerializationResultCanBeDeserialized();

        [Fact]
        public void SerializationResultHasCorrectValueAfterDeserialization() =>
            _testsBase.SerializationResultHasCorrectValueAfterDeserialization();

    }
}
