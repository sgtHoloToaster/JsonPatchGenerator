using JsonPatchGenerator.Interface.Models.Abstract;

namespace JsonPatchGenerator.Interface.Services
{
    public interface IPatchDocumentBuilderFactory<out TPatchDocument> where TPatchDocument : IPatchDocument
    {
        IPatchDocumentBuilder<TPatchDocument> Create();
    }
}
