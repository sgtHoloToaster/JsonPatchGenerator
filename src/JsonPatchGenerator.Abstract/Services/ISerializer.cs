using JsonPatchGenerator.Interface.Models;

namespace JsonPatchGenerator.Interface.Services
{
    public interface ISerializer
    {
        string Serialize(DiffDocument diff);
    }
}
