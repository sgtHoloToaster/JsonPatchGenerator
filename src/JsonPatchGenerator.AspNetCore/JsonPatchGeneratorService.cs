using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchGeneratorService : IJsonPatchGenerator<IJsonPatchDocument>
    {
        readonly Core.Services.JsonPatchGeneratorService _patchGenerator;

        public JsonPatchGeneratorService(ITypeResolver typeResolver, IPatchDocumentBuilderFactory<IJsonPatchDocumentWrapper> patchDocumentBuilderFactory)
        {
            _patchGenerator = new Core.Services.JsonPatchGeneratorService(typeResolver, patchDocumentBuilderFactory);
        }

        public IJsonPatchDocument Generate(object first, object second)
        {
            throw new NotImplementedException();
        }
    }
}
