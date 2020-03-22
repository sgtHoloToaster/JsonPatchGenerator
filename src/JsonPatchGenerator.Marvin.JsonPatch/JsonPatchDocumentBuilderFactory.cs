using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Marvin.JsonPatch.Abstract;

namespace JsonPatchGenerator.Marvin.JsonPatch
{
    public class JsonPatchDocumentBuilderFactory : IPatchDocumentBuilderFactory<IJsonPatchDocumentWrapper>
    {
        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> Create() =>
            new JsonPatchDocumentBuilder();
    }
}
