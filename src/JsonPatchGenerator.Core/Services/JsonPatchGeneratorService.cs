using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Core.Services
{
    public class JsonPatchGeneratorService : IJsonPatchGenerator
    {
        readonly ITypeResolver _typeResolver;
        const char _separator = '/';

        public JsonPatchGeneratorService(ITypeResolver typeResolver)
        {
            _typeResolver = typeResolver;
        }

        public DiffDocument GetDiff(object first, object second) =>
            new DiffDocument(GetObjectPatchOperations(first, second, string.Empty));

        private IEnumerable<Operation> GetObjectPatchOperations(object first, object second, string path) =>
            GetObjectPatchOperations(first, second, path, first.GetType());

        private IEnumerable<Operation> GetObjectPatchOperations(object first, object second, string path, Type type)
        {
            var operations = new List<Operation>();
            var properties = _typeResolver.GetProperties(type);
            foreach (var property in properties)
            {
                var firstValue = property.GetValue(first, _typeResolver);
                var secondValue = property.GetValue(second, _typeResolver);
                operations.AddRange(GetValuesDiff(firstValue, secondValue, $"{path}{_separator}{property.Name}", property.Type));
            }

            return operations;
        }

        private IEnumerable<Operation> GetValuesDiff(object firstValue, object secondValue, string path, Type propertyType)
        {
            if (firstValue != null && secondValue != null && !propertyType.IsPrimitive)
            {
                if (propertyType.IsArray)
                    return GetArrayPatchOperations(firstValue as Array, secondValue as Array, path, propertyType);
                else
                    return GetObjectPatchOperations(firstValue, secondValue, path, propertyType);
            }
            else if (!ReferenceEquals(firstValue, secondValue) && (!firstValue?.Equals(secondValue) ?? true))
                return new[] { new Operation(OperationType.Replace, secondValue, path) };
            else
                return Enumerable.Empty<Operation>();
        }

        private IEnumerable<Operation> GetArrayPatchOperations(Array firstArray, Array secondArray, string path, Type propertyType)
        {
            var operations = new List<Operation>();
            var firstArrayHashCodes = new ArrayHashIndexMap(firstArray, _typeResolver.GetHashCode);
            var secondArrayHashCodes = new ArrayHashIndexMap(secondArray, _typeResolver.GetHashCode);
            var toAdd = secondArrayHashCodes.Except(firstArrayHashCodes).ToArray();
            var toRemove = firstArrayHashCodes.Except(secondArrayHashCodes).ToArray();

            foreach (var hash in toAdd)
            {
                var indexes = secondArrayHashCodes.Map[hash];
                foreach (var index in indexes)
                {
                    if (index >= firstArray.Length)
                    {
                        operations.Add(new Operation(OperationType.Add, secondArray.GetValue(index), $"{path}{_separator}-"));
                    }
                    else if (toRemove.Contains(firstArrayHashCodes[index]))
                    {
                        var firstArrayValue = firstArray.GetValue(index);
                        var secondArrayValue = secondArray.GetValue(index);
                        var currentPath = $"{path}{_separator}{index}";
                        var elementType = propertyType.GetElementType();
                        operations.AddRange(GetValuesDiff(firstArrayValue, secondArrayValue, currentPath, elementType));
                    }
                    else
                    {
                        operations.Add(new Operation(OperationType.Add, secondArray.GetValue(index), $"{path}{_separator}{index}"));
                    }
                }
            }

            return operations;
        }
    }
}
