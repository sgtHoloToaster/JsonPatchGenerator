using JsonPatchGenerator.Marvin.JsonPatch;
using Marvin.JsonPatch;

namespace JsonPatchGenerator.Marvin.JsonPatch
{
    public class JsonPatchDocumentWrapper<T> : JsonPatchDocumentWrapper where T : class
    {
        public JsonPatchDocumentWrapper(JsonPatchDocument<T> jsonPatchDocument) : base(jsonPatchDocument)
        {
        }
    }
}
