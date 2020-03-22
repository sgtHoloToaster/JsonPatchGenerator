using JsonPatchGenerator.Core.Helpers;
using JsonPatchGenerator.Interface.Enums;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Marvin.JsonPatch.Abstract;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Marvin.JsonPatch
{
    public class JsonPatchDocumentWrapper : IJsonPatchDocumentWrapper
    {
        readonly IJsonPatchDocument _jsonPatchDocument;
        public JsonPatchDocumentWrapper(IJsonPatchDocument jsonPatchDocument)
        {
            _jsonPatchDocument = jsonPatchDocument;
        }

        public IEnumerable<Operation> Operations =>
            _jsonPatchDocument.GetOperations()
                .Select(o => new Operation(EnumsHelper.GetValueByEnumMemberAttribute<OperationType>(o.op), o.path, o.value, o.from))
                .ToList();

        public IJsonPatchDocument GetValue() =>
            _jsonPatchDocument;
    }
}
