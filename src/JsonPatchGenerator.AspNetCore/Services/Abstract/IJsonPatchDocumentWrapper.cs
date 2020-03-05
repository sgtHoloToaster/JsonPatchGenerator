using JsonPatchGenerator.Interface.Models.Abstract;
using Microsoft.AspNetCore.JsonPatch;

namespace JsonPatchGenerator.AspNetCore.Services.Abstract
{
    public interface IJsonPatchDocumentWrapper : IPatchDocument
    {
        IJsonPatchDocument GetValue();
    }
}
