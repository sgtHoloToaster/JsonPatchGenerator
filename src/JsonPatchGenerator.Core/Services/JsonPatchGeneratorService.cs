using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Interface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Core.Services
{
    public class JsonPatchGeneratorService : IJsonPatchGenerator
    {
        readonly ITypeResolver _typeResolver;
        readonly IPatchDocumentBuilderFactory _patchDocumentBuilderFactory;
        const string PathSeparator = "/";
        const string ArrayLastPositionLiteral = "-";

        public JsonPatchGeneratorService(ITypeResolver typeResolver, IPatchDocumentBuilderFactory patchDocumentBuilderFactory)
        {
            _typeResolver = typeResolver;
            _patchDocumentBuilderFactory = patchDocumentBuilderFactory;
        }

        public IPatchDocument GetDiff(object first, object second)
        {
            var builder = _patchDocumentBuilderFactory.Create();
            GetObjectPatchOperations(builder, first, second, string.Empty);
            return builder.Build();
        }

        private void GetObjectPatchOperations(IPatchDocumentBuilder builder, object first, object second, string path) =>
            GetObjectPatchOperations(builder, first, second, path, first.GetType());

        private void GetObjectPatchOperations(IPatchDocumentBuilder builder, object first, object second, string path, Type type)
        {
            var properties = _typeResolver.GetProperties(type);
            foreach (var property in properties)
            {
                var firstValue = property.GetValue(first, _typeResolver);
                var secondValue = property.GetValue(second, _typeResolver);
                GetValuesDiff(builder, firstValue, secondValue, ConcatPath(path, property.Name), property.Type);
            }
        }

        private void GetValuesDiff(IPatchDocumentBuilder builder, object firstValue, object secondValue, string path, Type propertyType)
        {
            if (firstValue != null && secondValue != null && !propertyType.IsPrimitive)
            {
                if (propertyType.IsArray)
                    GetArrayPatchOperations(builder, firstValue as Array, secondValue as Array, path, propertyType);
                else
                    GetObjectPatchOperations(builder, firstValue, secondValue, path, propertyType);
            }
            else if (!ReferenceEquals(firstValue, secondValue) && (!firstValue?.Equals(secondValue) ?? true))
                builder.AppendReplaceOperation(path, secondValue);
        }

        //TODO: refactor
        private void GetArrayPatchOperations(IPatchDocumentBuilder builder, Array firstArray, Array secondArray, string path, Type propertyType)
        {
            var firstArrayHashCodes = new ArrayHashIndexMap(firstArray, _typeResolver.GetHashCode);
            var secondArrayHashCodes = new ArrayHashIndexMap(secondArray, _typeResolver.GetHashCode);
            var toAdd = secondArrayHashCodes.Except(firstArrayHashCodes).ToArray();
            var toRemove = firstArrayHashCodes.Except(secondArrayHashCodes).ToArray();
            var offsets = new int[secondArray.Length];
            foreach (var hash in toRemove)
            {
                var rawIndexes = firstArrayHashCodes.Map[hash];
                foreach (var rawIndex in rawIndexes)
                {
                    var index = rawIndex + offsets[rawIndex];
                    if (index >= firstArray.Length || !toAdd.Contains(secondArrayHashCodes[index]))
                    {
                        builder.AppendRemoveOperation(ConcatPath(path, index));
                        for (var i = index + 1; i < offsets.Length; i++)
                            offsets[i]--;
                    }
                }
            }

            for (var index = 0; index < secondArray.Length; index++)
            {
                var indexWithOffset = index + offsets[index];
                if (indexWithOffset < firstArray.Length && firstArrayHashCodes[indexWithOffset] == secondArrayHashCodes[index])
                    continue;

                if (index >= firstArray.Length)
                    builder.AppendAddOperation(ConcatPath(path, ArrayLastPositionLiteral), secondArray.GetValue(index));
                else if (toRemove.Contains(firstArrayHashCodes[indexWithOffset]) && toAdd.Contains(secondArrayHashCodes[index]))
                {
                    var firstArrayValue = firstArray.GetValue(indexWithOffset);
                    var secondArrayValue = secondArray.GetValue(index);
                    var currentPath = ConcatPath(path, index);
                    var elementType = propertyType.GetElementType();
                    GetValuesDiff(builder, firstArrayValue, secondArrayValue, currentPath, elementType);
                }
                else if (firstArrayHashCodes.Map.TryGetValue(secondArrayHashCodes[index], out var indexes))
                {
                    var rawFromIndex = indexes.First(); // TODO: take into consideration cases with duplicate items
                    if (rawFromIndex >= offsets.Length)
                        continue;

                    var fromIndex = rawFromIndex + offsets[rawFromIndex];
                    if (fromIndex == index)
                        continue;

                    builder.AppendMoveOperation(ConcatPath(path, index), ConcatPath(path, fromIndex));
                    if (fromIndex > index)
                        for (var i = index + 1; i <= fromIndex; i++)
                            offsets[i]--;
                    else
                        for (var i = fromIndex; i < index; i++)
                            offsets[i]++;
                }
                else
                {
                    builder.AppendAddOperation(ConcatPath(path, index), secondArray.GetValue(index));
                    for (var i = index + 1; i < offsets.Length; i++)
                        offsets[i]--;
                }
            }
        }

        private string ConcatPath(params object[] pathParts) =>
            string.Join(PathSeparator, pathParts);
    }
}
