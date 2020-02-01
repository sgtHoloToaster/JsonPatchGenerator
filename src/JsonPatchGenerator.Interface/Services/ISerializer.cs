using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Models.Abstract;

namespace JsonPatchGenerator.Interface.Services
{
    public interface ISerializer
    {
        string Serialize(IPatchDocument diff);
    }
}
