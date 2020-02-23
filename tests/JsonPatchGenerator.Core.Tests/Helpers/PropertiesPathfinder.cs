using System;
using System.Collections;
using System.Linq;

namespace JsonPatchGenerator.Core.Tests.Helpers
{
    public static class PropertiesPathfinder
    {
        public const char Separator = '/';

        public static void SetValue(object obj, string path, object value)
        {
            var pathParts = path.Split(Separator)
                                .Where(p => !string.IsNullOrWhiteSpace(p))
                                .ToArray();
            var currentPathPart = pathParts[0];
            var isIndex = int.TryParse(currentPathPart, out var index);
            if (isIndex)
            {
                var isNested = pathParts.Length > 1;
                if (obj is Array array)
                {
                    if (isNested)
                        SetValue(array.GetValue(index), string.Join(Separator, pathParts.Skip(1)), value);
                    else
                        array.SetValue(value, index);
                }
                else if (obj is IList list)
                {
                    if (isNested)
                        SetValue(list[index], string.Join(Separator, pathParts.Skip(1)), value);
                    else
                        list[index] = value;
                }
                else
                    throw new NotImplementedException();
            }
            else
            {
                var currentProperty = obj.GetType().GetProperty(currentPathPart);
                var currentPropertyType = currentProperty.PropertyType;
                if (currentPropertyType.IsArray && pathParts.Length > 1)
                {
                    var currentValue = currentProperty.GetValue(obj);
                    SetValue(currentValue, string.Join(Separator, pathParts.Skip(1)), value);
                }
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
}