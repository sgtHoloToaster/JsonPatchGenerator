using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentGenerator : IJsonPatchGenerator<IJsonPatchDocument>
    {
        readonly IJsonPatchGenerator<IJsonPatchDocumentWrapper> _patchGenerator;

        public JsonPatchDocumentGenerator()
        {
            var typeResolver = new DefaultTypeResolver();
            var buildersFactory = new JsonPatchDocumentBuilderFactory();
            _patchGenerator = new JsonPatchGeneratorService<IJsonPatchDocumentWrapper>(typeResolver, buildersFactory);
        }

        public JsonPatchDocumentGenerator(IJsonPatchGenerator<IJsonPatchDocumentWrapper> jsonPatchGenerator)
        {
            _patchGenerator = jsonPatchGenerator;
        }

        public IJsonPatchDocument Generate(object first, object second) =>
            _patchGenerator.Generate(first, second)
                .GetValue();

        public IJsonPatchDocument Generate<T1>(T1 first, T1 second)
        {
            throw new NotImplementedException();
        }
    }
}
