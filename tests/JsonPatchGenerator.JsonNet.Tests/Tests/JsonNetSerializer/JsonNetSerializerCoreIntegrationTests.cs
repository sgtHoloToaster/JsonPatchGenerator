using AutoMoqCore;
using JsonPatchGenerator.Json.NET.Serializer.Service;
using Xunit;
using JsonPatchGenerator.Core.Services;

namespace JsonPatchGenerator.JsonNet.Tests.Tests
{
    public class JsonNetSerializerCoreIntegrationTests : IJsonNetSerializerTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();
        readonly JsonNetSerializerTestsBase _testsBase;
        public JsonNetSerializerCoreIntegrationTests()
        {
            _testsBase = new JsonNetSerializerTestsBase(GetTarget);
        }

        private JsonNetSerializer GetTarget() =>
            new JsonNetSerializer(new DefaultPatchDocumentBuilderFactory());

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
