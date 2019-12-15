using JsonPatchGenerator.Interface.Models;

namespace JsonPatchGenerator.Interface.Services
{
    public interface IJsonPatchGenerator
    {
        DiffDocument GetDiff(object first, object second);
    }
}
