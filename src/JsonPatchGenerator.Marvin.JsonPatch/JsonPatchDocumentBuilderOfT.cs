using JsonPatchGenerator.Core.Helpers;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Marvin.JsonPatch.Abstract;
using Marvin.JsonPatch;
using Marvin.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace JsonPatchGenerator.Marvin.JsonPatch
{
    public class JsonPatchDocumentBuilder<T> : IPatchDocumentBuilder<IJsonPatchDocumentWrapper> where T : class
    {
        readonly List<Operation<T>> _operations = new List<Operation<T>>();

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendAddOperation<T1>(string path, T1 value)
        {
            _operations.Add(new Operation<T>("add", path, null, value));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendCopyOperation(string path, string from)
        {
            _operations.Add(new Operation<T>("copy", path, from));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendMoveOperation(string path, string from)
        {
            _operations.Add(new Operation<T>("move", path, from));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendOperation<T1>(Interface.Enums.OperationType operationType, string path, T1 value, string from)
        {
            _operations.Add(new Operation<T>(EnumsHelper.GetEnumMemberAttributeValue(operationType), path, from, value));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendRemoveOperation(string path)
        {
            _operations.Add(new Operation<T>("remove", path, null));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendReplaceOperation<T1>(string path, T1 value)
        {
            _operations.Add(new Operation<T>("replace", path, null, value));
            return this;
        }

        public IPatchDocumentBuilder<IJsonPatchDocumentWrapper> AppendTestOperation<T1>(string path, T1 value)
        {
            _operations.Add(new Operation<T>("test", path, null, value));
            return this;
        }

        public IJsonPatchDocumentWrapper Build() =>
            new JsonPatchDocumentWrapper<T>(new JsonPatchDocument<T>(_operations, new DefaultContractResolver()));
    }
}
