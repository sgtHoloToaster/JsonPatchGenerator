using JsonPatchGenerator.Interface.Services;
using System;

namespace JsonPatchGenerator.Core.Services
{
    public class DefaultPatchDocumentBuilderFactory : IPatchDocumentBuilderFactory
    {
        public IPatchDocumentBuilder Create()
        {
            throw new NotImplementedException();
        }
    }
}
