using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Core.Helpers;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentBuilder : IPatchDocumentBuilder<IJsonPatchDocumentWrapper>
    {
        readonly List<Operation> _operations = new List<Operation>();

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendAddOperation<T>(string path, T value)
        {
            _operations.Add(new Operation("add", path, null, value));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendCopyOperation(string path, string from)
        {
            _operations.Add(new Operation("copy", path, from));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendMoveOperation(string path, string from)
        {
            _operations.Add(new Operation("move", path, from));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendOperation<T>(Interface.Enums.OperationType operationType, string path, T value, string from)
        {
            _operations.Add(new Operation(EnumsHelper.GetEnumMemberAttributeValue(operationType), path, from, value));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendRemoveOperation(string path)
        {
            _operations.Add(new Operation("remove", path, null));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendReplaceOperation<T>(string path, T value)
        {
            _operations.Add(new Operation("replace", path, null, value));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendTestOperation<T>(string path, T value)
        {
            _operations.Add(new Operation("test", path, null, value));
            return this;
        }

        public IJsonPatchDocumentWrapper Build() =>
            new JsonPatchDocumentWrapper(new JsonPatchDocument(_operations, new DefaultContractResolver()));
    }
}
