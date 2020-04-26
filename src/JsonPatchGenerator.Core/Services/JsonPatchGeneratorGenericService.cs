using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;

namespace JsonPatchGenerator.Core.Services
{
    public class JsonPatchGeneratorGenericService : JsonPatchGeneratorGenericService<IPatchDocument>
    {
        public JsonPatchGeneratorGenericService(ITypeResolver typeResolver, IPatchDocumentBuilderFactoryGeneric<IPatchDocument> patchDocumentBuilderFactory) : base(typeResolver, patchDocumentBuilderFactory) { }
    }

    public class JsonPatchGeneratorGenericService<T> : JsonPatchGeneratorService<T>, IJsonPatchGeneratorGeneric<T> where T : IPatchDocument
    {
        readonly IPatchDocumentBuilderFactoryGeneric<T> _patchDocumentBuilderFactory;

        public JsonPatchGeneratorGenericService(ITypeResolver typeResolver, IPatchDocumentBuilderFactoryGeneric<T> patchDocumentBuilderFactory) : base(typeResolver, patchDocumentBuilderFactory)
        {
            _patchDocumentBuilderFactory = patchDocumentBuilderFactory;
        }

        public T Generate<T1>(T1 first, T1 second) where T1 : class
        {
            var builder = _patchDocumentBuilderFactory.Create<T1>();
            AppendObjectPatchOperations(builder, first, second, string.Empty);
            return builder.Build();
        }
    }
}
