using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using OneType;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentGenerator : IJsonPatchGeneratorGeneric<IJsonPatchDocument>
    {
        readonly IJsonPatchGeneratorGeneric<IJsonPatchDocumentWrapper> _patchGenerator;

        public JsonPatchDocumentGenerator()
        {
            var typeResolver = new DefaultTypeResolver();
            var buildersFactory = new JsonPatchDocumentBuilderFactory();
            _patchGenerator = new JsonPatchGeneratorGenericService<IJsonPatchDocumentWrapper>(typeResolver, buildersFactory);
        }

        public JsonPatchDocumentGenerator(IJsonPatchGeneratorGeneric<IJsonPatchDocumentWrapper> jsonPatchGenerator)
        {
            _patchGenerator = jsonPatchGenerator;
        }

        public IJsonPatchDocument Generate(object first, object second) =>
            (_patchGenerator as IJsonPatchGenerator<IJsonPatchDocumentWrapper>).Generate(first, second)
                .GetValue();

        public IJsonPatchDocument Generate<T1>(T1 first, T1 second) where T1 : class =>
            _patchGenerator.Generate(first, second)
                .GetValue();
    }
}
