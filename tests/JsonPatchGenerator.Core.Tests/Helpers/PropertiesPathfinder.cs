using System;
using System.Linq;

namespace JsonPatchGenerator.Core.Tests.Helpers
{
    public static class PropertiesPathfinder
    {
        public const char Separator = '/';

        public static void SetValue(object obj, string path, object value)
        {
            var pathParts = path.Split(Separator);
            var currentPropertyName = pathParts[0];
            var currentProperty = obj.GetType().GetProperty(currentPropertyName);
            var currentPropertyType = currentProperty.PropertyType;
            if (currentPropertyType.IsArray)
                throw new NotImplementedException();
            else if (!currentPropertyType.IsPrimitive && pathParts.Length > 1 /* the target property is nested in the current one*/)
            {
                var propertyValue = currentProperty.GetValue(obj);
                if (propertyValue == null)
                    currentProperty.SetValue(obj, Activator.CreateInstance(currentPropertyType));

                var newPath = string.Join(Separator, pathParts.Skip(1));
                SetValue(currentProperty.GetValue(obj), newPath, value);
            }
            else
                currentProperty.SetValue(obj, value);
        }
    }
}