using JsonPatchGenerator.Interface.Models.Abstract;
using Marvin.JsonPatch;

namespace JsonPatchGenerator.Marvin.Json.Abstract
{
    public interface IJsonPatchDocumentWrapper : IPatchDocument
    {
        IJsonPatchDocument GetValue();
    }
}
