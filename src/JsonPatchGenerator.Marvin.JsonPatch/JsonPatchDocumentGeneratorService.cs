using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Marvin.Json.Abstract;
using Marvin.JsonPatch;

namespace JsonPatchGenerator.Marvin.Json
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
