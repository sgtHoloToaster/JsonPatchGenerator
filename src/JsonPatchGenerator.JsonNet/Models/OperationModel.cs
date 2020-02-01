using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.JsonNet.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonPatchGenerator.JsonNet.Models
{
    internal class OperationModel
    {
        [JsonProperty(PropertyName = "op")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationTypeEnum Type { get; set; }

        [JsonProperty(PropertyName = "from", NullValueHandling = NullValueHandling.Ignore)]
        public string From { get; set; }

        [JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        public OperationModel() { }

        public OperationModel(Operation operation)
        {
            Type = (OperationTypeEnum)(int)operation.Type;
            From = operation.From;
            Value = operation.Value;
            Path = operation.Path;
        }

        public static explicit operator Operation(OperationModel operation) =>
            new Operation((OperationType)(int)operation.Type, operation.Value, operation.Path, operation.From);
    }
}
