using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Interface.Services;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentBuilderFactory : IPatchDocumentBuilderFactoryGeneric<IJsonPatchDocumentWrapper>
    {
        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> Create() =>
            new JsonPatchDocumentBuilder();

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> Create<T1>() where T1 : class =>
            new JsonPatchDocumentBuilder<T1>();
    }
}
