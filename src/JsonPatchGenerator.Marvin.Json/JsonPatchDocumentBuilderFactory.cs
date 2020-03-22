using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Marvin.Json.Abstract;

namespace JsonPatchGenerator.Marvin.Json
{
    public class JsonPatchDocumentBuilderFactory : IPatchDocumentBuilderFactory<IJsonPatchDocumentWrapper>
    {
        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> Create() =>
            new JsonPatchDocumentBuilder();
    }
}
