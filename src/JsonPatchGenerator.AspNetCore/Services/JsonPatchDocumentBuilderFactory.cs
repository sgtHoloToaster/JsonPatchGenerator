using JsonPatchGenerator.AspNetCore.Services.Abstract;
using JsonPatchGenerator.Interface.Services;

namespace JsonPatchGenerator.AspNetCore.Services
{
    public class JsonPatchDocumentBuilderFactory : IPatchDocumentBuilderFactory<IJsonPatchDocumentWrapper>
    {
        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> Create() =>
            new JsonPatchDocumentBuilder();
    }
}
