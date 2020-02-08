using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Interface.Enums;
using System;
using JsonPatchGenerator.Interface.Models;
using System.Collections.Generic;

namespace JsonPatchGenerator.Core.Services
{
    public class DefaultPatchDocumentBuilder : IPatchDocumentBuilder<IPatchDocument>
    {
        readonly List<Operation> _operations = new List<Operation>();

        public IPatchDocumentBuilder<IPatchDocument> AppendAddOperation<T>(string path, T value)
        {
            _operations.Add(new Operation(OperationType.Add, path, value));
            return this;
        }

        public IPatchDocumentBuilder<IPatchDocument> AppendCopyOperation(string path, string from)
        {
            _operations.Add(new Operation(OperationType.Copy, path, null, from));
            return this;
        }

        public IPatchDocumentBuilder<IPatchDocument> AppendMoveOperation(string path, string from)
        {
            _operations.Add(new Operation(OperationType.Move, path, null, from));
            return this;
        }

        public IPatchDocumentBuilder<IPatchDocument> AppendOperation<T>(OperationType operationType, string path, T value, string from)
        {
            _operations.Add(new Operation(operationType, path, value, from));
            return this;
        }

        public IPatchDocumentBuilder<IPatchDocument> AppendRemoveOperation(string path)
        {
            _operations.Add(new Operation(OperationType.Remove, path));
            return this;
        }

        public IPatchDocumentBuilder<IPatchDocument> AppendReplaceOperation<T>(string path, T value)
        {
            _operations.Add(new Operation(OperationType.Replace, path, value));
            return this;
        }

        public IPatchDocumentBuilder<IPatchDocument> AppendTestOperation<T>(string path, T value)
        {
            _operations.Add(new Operation(OperationType.Test, path, value));
            return this;
        }

        public IPatchDocument Build() =>
            new PatchDocument(_operations);
    }
}
