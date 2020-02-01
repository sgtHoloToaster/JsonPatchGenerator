using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Enums;
using Newtonsoft.Json;

namespace JsonPatchGenerator.JsonNet.Models
{
    internal class OperationFullModel : OperationWithFromModel
    {
        public OperationFullModel() { }

        public OperationFullModel(Operation operation) : base(operation)
        {
            Value = operation.Value;
        }

        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }

        public static explicit operator Operation(OperationFullModel operation) =>
            new Operation((OperationType)(int)operation.Type, operation.Path, operation.Value, operation.From);
    }
}
