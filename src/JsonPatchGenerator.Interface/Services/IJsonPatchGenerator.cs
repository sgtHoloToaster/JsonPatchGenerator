using JsonPatchGenerator.Interface.Models.Abstract;

namespace JsonPatchGenerator.Interface.Services
{
    public interface IJsonPatchGenerator<T>
    {
        T GetDiff(object first, object second);
    }
}
