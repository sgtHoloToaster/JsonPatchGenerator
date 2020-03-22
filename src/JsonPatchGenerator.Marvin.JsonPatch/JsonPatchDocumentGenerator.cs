using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Marvin.JsonPatch.Abstract;
using Marvin.JsonPatch;

namespace JsonPatchGenerator.Marvin.JsonPatch
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
    }
}
