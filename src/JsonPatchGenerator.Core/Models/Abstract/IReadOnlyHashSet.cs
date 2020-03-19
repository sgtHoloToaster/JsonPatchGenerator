using System;
using System.Collections.Generic;
using System.Text;

namespace JsonPatchGenerator.Core.Models.Abstract
{
    internal interface IReadOnlyHashSet<T> : IReadOnlyCollection<T>
    {
        bool Contains(T value);
    }
}
