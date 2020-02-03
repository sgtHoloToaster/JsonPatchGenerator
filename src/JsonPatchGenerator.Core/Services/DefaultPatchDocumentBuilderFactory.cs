using JsonPatchGenerator.Interface.Services;

namespace JsonPatchGenerator.Core.Services
{
    public class DefaultPatchDocumentBuilderFactory : IPatchDocumentBuilderFactory
    {
        public IPatchDocumentBuilder Create() =>
            new DefaultPatchDocumentBuilder();
    }
}
