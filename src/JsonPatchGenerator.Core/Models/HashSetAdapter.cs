using System.Collections.Generic;
using JsonPatchGenerator.Core.Models.Abstract;

namespace JsonPatchGenerator.Core.Models
{
    internal class HashSetReadOnlyAdapter<T> : HashSet<T>, IReadOnlyHashSet<T>
    {
    }
}
