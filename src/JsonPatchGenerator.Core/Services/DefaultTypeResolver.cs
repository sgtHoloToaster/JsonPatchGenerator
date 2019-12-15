using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Core.Services
{
    public class DefaultTypeResolver : ITypeResolver
    {
        public IEnumerable<ObjectProperty> GetProperties(Type type) =>
            type.GetProperties()
                .Select(p => new ObjectProperty(p))
                .ToList();

        public object GetValue(object obj, ObjectProperty property)
        {
            var propertyInfo = obj.GetType().GetProperty(property.Name);
            return propertyInfo.GetValue(obj);
        }
    }
}