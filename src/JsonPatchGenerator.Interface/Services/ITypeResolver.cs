using JsonPatchGenerator.Core.Models;
using System;
using System.Collections.Generic;

namespace JsonPatchGenerator.Interface.Services
{
    public interface ITypeResolver
    {
        IEnumerable<ObjectProperty> GetProperties(Type type);

        object GetValue(object obj, ObjectProperty property);

        int GetHashCode(object obj);
    }
}
