using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Interface.Enums;
using System;

namespace JsonPatchGenerator.Core.Services
{
    public class DefaultPatchDocumentBuilder : IPatchDocumentBuilder
    {
        public IPatchDocumentBuilder AppendAddOperation<T>(string path, T value)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder AppendCopyOperation(string path, string from)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder AppendMoveOperation(string path, string from)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder AppendOperation(OperationType operationType, string path, object value, string from)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder AppendOperation<T>(OperationType operationType, string path, T value, string from)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder AppendRemoveOperation(string path)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder AppendReplaceOperation<T>(string path, T value)
        {
            throw new NotImplementedException();
        }

        public IPatchDocumentBuilder AppendTestOperation<T>(string path, T value)
        {
            throw new NotImplementedException();
        }

        public IPatchDocument Build()
        {
            throw new NotImplementedException();
        }
    }
}
