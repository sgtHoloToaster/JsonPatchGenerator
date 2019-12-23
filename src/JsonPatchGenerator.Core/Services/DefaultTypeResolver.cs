using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JsonPatchGenerator.Core.Services
{
    public class DefaultTypeResolver : ITypeResolver
    {
        public IEnumerable<ObjectProperty> GetProperties(Type type) =>
            GetRawProperties(type).Select(p => new ObjectProperty(p))
                                  .ToList();

        private IEnumerable<PropertyInfo> GetRawProperties(Type type) =>
            type.GetProperties();

        public object GetValue(object obj, ObjectProperty property)
        {
            var propertyInfo = obj.GetType().GetProperty(property.Name);
            return propertyInfo.GetValue(obj);
        }
        
        // TODO: maybe move it to a separate universal equalityComparer
        public int GetHashCode(object obj)
        {
            if (obj == null)
                return int.MinValue;

            var type = obj.GetType();
            if (type.IsPrimitive || type == typeof(string))
                return obj.GetHashCode();

            var properties = GetRawProperties(type);
            var hash = 17;
            unchecked
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(obj);
                    hash *= 23 + GetHashCode(value);
                }
            }

            return hash;
        }
    }
}