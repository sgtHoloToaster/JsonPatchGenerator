using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;

namespace JsonPatchGenerator.Core.Services
{
    public class DefaultPatchDocumentBuilderFactory : IPatchDocumentBuilderFactory<IPatchDocument>
    {
        public IPatchDocumentBuilder<IPatchDocument> Create() =>
            new DefaultPatchDocumentBuilder();
    }
}
