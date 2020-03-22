using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Marvin.Json.Abstract;
using Marvin.JsonPatch;
using System;
using System.Collections.Generic;

namespace JsonPatchGenerator.Marvin.Json
{
    public class JsonPatchDocumentWrapper : IJsonPatchDocumentWrapper
    {
        readonly IJsonPatchDocument _jsonPatchDocument;
        public JsonPatchDocumentWrapper(IJsonPatchDocument jsonPatchDocument)
        {
            _jsonPatchDocument = jsonPatchDocument;
        }

        public IEnumerable<Operation> Operations =>
            throw new NotImplementedException();

        public IJsonPatchDocument GetValue() =>
            throw new NotImplementedException();
    }
}
