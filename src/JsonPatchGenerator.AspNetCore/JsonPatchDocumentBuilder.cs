using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Collections.Generic;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentBuilder : IPatchDocumentBuilder<IJsonPatchDocumentWrapper>
    {
        readonly List<Operation> _operations = new List<Operation>();

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendAddOperation<T>(string path, T value)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendCopyOperation(string path, string from)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendMoveOperation(string path, string from)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendOperation<T>(Interface.Enums.OperationType operationType, string path, T value, string from)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendRemoveOperation(string path)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendReplaceOperation<T>(string path, T value)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendTestOperation<T>(string path, T value)
        {
            throw new NotImplementedException();
        }

        public IJsonPatchDocumentWrapper Build()
        {
            throw new NotImplementedException();
        }
    }
}
