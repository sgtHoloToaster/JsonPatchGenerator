using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace JsonPatchGenerator.Core.Tests.Helpers
{
    public static class PropertiesPathfinder
    {
        public const char Separator = '/';
        private static readonly Regex _arrayIndexRegex = new Regex(@"\[([0-9]+)\]", RegexOptions.Compiled);

        public static void SetValue(object obj, string path, object value)
        {
            var pathParts = path.Split(Separator)
                                .Where(p => !string.IsNullOrWhiteSpace(p))
                                .ToArray();
            var currentPathPart = pathParts[0];
            var indexRegexMatch = _arrayIndexRegex.Match(currentPathPart);
            var index = indexRegexMatch.Success ? (int?)int.Parse(indexRegexMatch.Groups[1].Value) : null;
            var currentPropertyName = index.HasValue ? _arrayIndexRegex.Replace(currentPathPart, string.Empty) : currentPathPart;
            var currentProperty = obj.GetType().GetProperty(currentPropertyName);
            var currentPropertyType = currentProperty.PropertyType;
            if (currentPropertyType.IsArray && pathParts.Length > 1)
            {
                throw new NotImplementedException();
            }
            else if (currentPropertyType.IsArray)
            {
                var propertyValue = currentProperty.GetValue(obj) as Array;
                propertyValue.SetValue(value, index.Value);
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