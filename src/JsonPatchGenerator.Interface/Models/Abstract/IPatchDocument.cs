using JsonPatchGenerator.Interface.Services;
using System.Collections.Generic;

namespace JsonPatchGenerator.Interface.Models.Abstract
{
    public interface IPatchDocument
    {
        IEnumerable<Operation> Operations { get; }

        string Serialize(ISerializer serializer);
    }
}