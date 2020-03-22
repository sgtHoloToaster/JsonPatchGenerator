using JsonPatchGenerator.Interface.Models.Abstract;
using Marvin.JsonPatch;

namespace JsonPatchGenerator.Marvin.JsonPatch.Abstract
{
    public interface IJsonPatchDocumentWrapper : IPatchDocument
    {
        IJsonPatchDocument GetValue();
    }
}
