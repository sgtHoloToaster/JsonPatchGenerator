using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentGenerator : IJsonPatchGenerator<IJsonPatchDocument>
    {
        readonly IJsonPatchGenerator<IJsonPatchDocumentWrapper> _patchGenerator;

        public JsonPatchDocumentGenerator(IJsonPatchGenerator<IJsonPatchDocumentWrapper> jsonPatchGenerator)
        {
            _patchGenerator = jsonPatchGenerator;
        }

        public IJsonPatchDocument Generate(object first, object second) =>
            _patchGenerator.Generate(first, second)
                .GetValue();
    }
}
