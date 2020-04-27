using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Core.Services
{
    public class JsonPatchGeneratorService : JsonPatchGeneratorService<IPatchDocument>
    {
        public JsonPatchGeneratorService(ITypeResolver typeResolver, IPatchDocumentBuilderFactory<IPatchDocument> patchDocumentBuilderFactory) : base(typeResolver, patchDocumentBuilderFactory)
        {
        }
    }

    public class JsonPatchGeneratorService<T> : IJsonPatchGenerator<T> where T : IPatchDocument
    {
        readonly ITypeResolver _typeResolver;
        readonly IPatchDocumentBuilderFactory<T> _patchDocumentBuilderFactory;
        const string PathSeparator = "/";
        const string ArrayLastPositionLiteral = "-";

        public JsonPatchGeneratorService(ITypeResolver typeResolver, IPatchDocumentBuilderFactory<T> patchDocumentBuilderFactory)
        {
            _typeResolver = typeResolver;
            _patchDocumentBuilderFactory = patchDocumentBuilderFactory;
        }

        public T Generate(object first, object second)
        {
            var builder = _patchDocumentBuilderFactory.Create();
            AppendObjectPatchOperations(builder, first, second, string.Empty);
            return builder.Build();
        }

        protected void AppendObjectPatchOperations(IPatchDocumentBuilder<T> builder, object first, object second, string path) =>
            AppendObjectPatchOperations(builder, first, second, path, first.GetType());

        private void AppendObjectPatchOperations(IPatchDocumentBuilder<T> builder, object first, object second, string path, Type type)
        {
            var properties = _typeResolver.GetProperties(type);
            foreach (var property in properties)
            {
                var firstValue = property.GetValue(first, _typeResolver);
                var secondValue = property.GetValue(second, _typeResolver);
                AppendPatchOperations(builder, firstValue, secondValue, ConcatPath(path, property.Name), property.Type);
            }
        }

        private void AppendPatchOperations(IPatchDocumentBuilder<T> builder, object firstValue, object secondValue, string path, Type propertyType)
        {
            if (firstValue != null && secondValue != null && !propertyType.IsPrimitive && propertyType != typeof(string))
            {
                if (propertyType.IsArray)
                    AppendArrayPatchOperations(builder, firstValue as Array, secondValue as Array, path, propertyType);
                else if (typeof(IEnumerable).IsAssignableFrom(propertyType))
                    AppendArrayPatchOperations(builder, firstValue as IEnumerable, secondValue as IEnumerable, path, propertyType);
                else
                    AppendObjectPatchOperations(builder, firstValue, secondValue, path, propertyType);
            }
            else if (!ReferenceEquals(firstValue, secondValue) && (!firstValue?.Equals(secondValue) ?? true))
                builder.AppendReplaceOperation(path, secondValue);
        }

        // TODO: find a more efficient way to process ienumerable
        private void AppendArrayPatchOperations(IPatchDocumentBuilder<T> builder, IEnumerable first, IEnumerable second, string path, Type propertyType)
        {
            Array ToArray(IEnumerable array) =>
                array.Cast<object>().ToArray() as Array;

            AppendArrayPatchOperations(builder, ToArray(first), ToArray(second), path, propertyType);
        }

        //TODO: refactor
        private void AppendArrayPatchOperations(IPatchDocumentBuilder<T> builder, Array firstArray, Array secondArray, string path, Type propertyType)
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
                    var elementType = propertyType.GetElementType() /*array*/ ?? propertyType.GetGenericArguments()[0] /*ienumerable*/; // TODO: move this logic to a separate method and make it more readable
                    AppendPatchOperations(builder, firstArrayValue, secondArrayValue, currentPath, elementType);
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
