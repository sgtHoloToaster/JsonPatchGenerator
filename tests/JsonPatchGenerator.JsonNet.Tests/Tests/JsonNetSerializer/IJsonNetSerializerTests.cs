namespace JsonPatchGenerator.JsonNet.Tests.Tests
{
    public interface IJsonNetSerializerTests
    {
        void CanDeserialize();
        void CanSerialize();
        void DeserializationResultHasCorrectValue();
        void SerializationResultCanBeDeserialized();
        void SerializationResultHasCorrectValueAfterDeserialization();
    }
}