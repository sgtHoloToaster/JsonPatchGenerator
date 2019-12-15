using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Services;
using System.Collections.Generic;

namespace JsonPatchGenerator.Core.Services
{
    public class JsonPatchGeneratorService : IJsonPatchGenerator
    {
        readonly ITypeResolver _typeResolver;

        public JsonPatchGeneratorService(ITypeResolver typeResolver)
        {
            _typeResolver = typeResolver;
        }

        public DiffDocument GetDiff(object first, object second) =>
            GetDiff(first, second, string.Empty);

        private DiffDocument GetDiff(object first, object second, string path)
        {
            var properties = _typeResolver.GetProperties(first.GetType());
            var operations = new List<Operation>();
            foreach (var property in properties)
            {
                var firstValue = property.GetValue(first, _typeResolver);
                var secondValue = property.GetValue(second, _typeResolver);
                if (!ReferenceEquals(firstValue, secondValue) && (!firstValue?.Equals(secondValue) ?? true))
                    operations.Add(new Operation {
                        Path = $"{path}/{property.Name}",
                        Type = OperationType.Replace,
                        Value = secondValue
                    });
            }

            return new DiffDocument { Operations = operations };
        }
    }
}
