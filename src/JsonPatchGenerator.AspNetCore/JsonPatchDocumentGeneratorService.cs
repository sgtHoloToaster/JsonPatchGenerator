using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentGeneratorService : IJsonPatchGenerator<IJsonPatchDocument>
    {
        readonly IJsonPatchGenerator<IJsonPatchDocumentWrapper> _patchGenerator;

        public JsonPatchDocumentGeneratorService(IJsonPatchGenerator<IJsonPatchDocumentWrapper> jsonPatchGenerator)
        {
            _patchGenerator = jsonPatchGenerator;
        }

        public IJsonPatchDocument Generate(object first, object second)
        {
            throw new NotImplementedException();
        }
    }
}
