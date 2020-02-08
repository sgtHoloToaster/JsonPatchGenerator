using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Interface.Services;
using System;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentBuilderFactory : IPatchDocumentBuilderFactory<IJsonPatchDocumentWrapper>
    {
        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> Create()
        {
            throw new NotImplementedException();
        }
    }
}
