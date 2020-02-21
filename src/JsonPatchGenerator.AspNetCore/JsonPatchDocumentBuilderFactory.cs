using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Interface.Services;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentBuilderFactory : IPatchDocumentBuilderFactory<IJsonPatchDocumentWrapper>
    {
        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> Create() =>
            new JsonPatchDocumentBuilder();
    }
}
