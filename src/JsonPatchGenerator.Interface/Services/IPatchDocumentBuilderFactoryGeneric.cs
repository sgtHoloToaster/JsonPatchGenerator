using JsonPatchGenerator.Interface.Models.Abstract;

namespace JsonPatchGenerator.Interface.Services
{
    public interface IPatchDocumentBuilderFactoryGeneric<out TPatchDocument> : IPatchDocumentBuilderFactory<TPatchDocument> where TPatchDocument : IPatchDocument 
    {
        IPatchDocumentBuilder<TPatchDocument> Create<T1>();
    }
}
