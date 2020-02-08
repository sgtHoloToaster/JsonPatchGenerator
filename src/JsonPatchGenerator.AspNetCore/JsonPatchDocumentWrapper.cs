using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentWrapper : IJsonPatchDocumentWrapper
    {
        public IEnumerable<Operation> Operations => throw new NotImplementedException();

        public IJsonPatchDocument GetValue()
        {
            throw new NotImplementedException();
        }

        public string Serialize(ISerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
