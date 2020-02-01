using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Interface.Enums;
using System;
using JsonPatchGenerator.Interface.Models;
using System.Collections.Generic;

namespace JsonPatchGenerator.Core.Services
{
    public class DefaultPatchDocumentBuilder : IPatchDocumentBuilder
    {
        readonly List<Operation> _operations = new List<Operation>();

        public IPatchDocumentBuilder AppendAddOperation<T>(string path, T value)
        {
            _operations.Add(new Operation(OperationType.Add, path, value));
            return this;
        }

        public IPatchDocumentBuilder AppendCopyOperation(string path, string from)
        {
            _operations.Add(new Operation(OperationType.Copy, path, null, from));
            return this;
        }

        public IPatchDocumentBuilder AppendMoveOperation(string path, string from)
        {
            _operations.Add(new Operation(OperationType.Move, path, null, from));
            return this;
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
            _operations.Add(new Operation(OperationType.Remove, path));
            return this;
        }

        public IPatchDocumentBuilder AppendReplaceOperation<T>(string path, T value)
        {
            _operations.Add(new Operation(OperationType.Replace, path, value));
            return this;
        }

        public IPatchDocumentBuilder AppendTestOperation<T>(string path, T value)
        {
            _operations.Add(new Operation(OperationType.Test, path, value));
            return this;
        }

        public IPatchDocument Build() =>
            new PatchDocument(_operations);
    }
}
