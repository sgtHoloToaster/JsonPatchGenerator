using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Marvin.Json.Abstract;
using Marvin.JsonPatch;
using System;

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
            throw new NotImplementedException();
    }
}
