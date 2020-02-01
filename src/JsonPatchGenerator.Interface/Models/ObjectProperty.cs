using JsonPatchGenerator.Interface.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JsonPatchGenerator.Core.Models
{
    public class ObjectProperty
    {
        public Type Type { get; set;  }

        public string Name { get; set;  }

        public ObjectProperty(PropertyInfo propertyInfo)
        {
            Type = propertyInfo.PropertyType;
            Name = propertyInfo.Name;
        }

        public object GetValue(object obj, ITypeResolver typeResolver) =>
            typeResolver.GetValue(obj, this);

        public override bool Equals(object obj) =>
            Equals(obj as ObjectProperty);

        public bool Equals(ObjectProperty property)
        {
            if (property == null)
                return false;

            if (ReferenceEquals(this, property))
                return true;

            return property.Type == Type
                && property.Name == Name;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            unchecked
            {
                hash *= 23 + (Type?.GetHashCode() ?? 0);
                hash *= 23 + (Type?.GetHashCode() ?? 0);
            }

            return hash;
        }
    }
}
