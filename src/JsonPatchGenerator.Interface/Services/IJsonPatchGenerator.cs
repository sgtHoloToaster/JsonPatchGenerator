using JsonPatchGenerator.Interface.Models.Abstract;

namespace JsonPatchGenerator.Interface.Services
{
    public interface IJsonPatchGenerator
    {
        IPatchDocument GetDiff(object first, object second);
    }
}
