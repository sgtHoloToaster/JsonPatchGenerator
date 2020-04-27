using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Marvin.JsonPatch.Abstract;

namespace JsonPatchGenerator.Marvin.JsonPatch
{
    public class JsonPatchDocumentBuilderFactory : IPatchDocumentBuilderFactoryGeneric<IJsonPatchDocumentWrapper>
    {
        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> Create() =>
            new JsonPatchDocumentBuilder();

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> Create<T1>() where T1 : class =>
            new JsonPatchDocumentBuilder<T1>();
    }
}
