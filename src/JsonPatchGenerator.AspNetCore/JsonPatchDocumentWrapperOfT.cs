using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Core.Helpers;
using JsonPatchGenerator.Interface.Enums;
using JsonPatchGenerator.Interface.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchDocumentWrapper<T> : JsonPatchDocumentWrapper where T : class
    {
        public JsonPatchDocumentWrapper(JsonPatchDocument<T> jsonPatchDocument) : base(jsonPatchDocument)
        {
        }
    }
}
