using JsonPatchGenerator.AspNetCore.Services.Abstract;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;

namespace JsonPatchGenerator.AspNetCore.Services
{
    public class JsonPatchDocumentGeneratorService : IJsonPatchGenerator<IJsonPatchDocument>
    {
        readonly IJsonPatchGenerator<IJsonPatchDocumentWrapper> _patchGenerator;

        public JsonPatchDocumentGeneratorService(IJsonPatchGenerator<IJsonPatchDocumentWrapper> jsonPatchGenerator)
        {
            _patchGenerator = jsonPatchGenerator;
        }

        public IJsonPatchDocument Generate(object first, object second) =>
            _patchGenerator.Generate(first, second)
                .GetValue();
    }
}
