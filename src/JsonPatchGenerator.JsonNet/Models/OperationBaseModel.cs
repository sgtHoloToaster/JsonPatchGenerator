using JsonPatchGenerator.Interface.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonPatchGenerator.JsonNet.Models
{
    internal class OperationBaseModel
    {
        [JsonProperty(PropertyName = "op")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OperationTypeEnum Type { get; set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        public OperationBaseModel() { }

        public OperationBaseModel(Operation operation)
        {
            Type = (OperationTypeEnum)(int)operation.Type;
            Path = operation.Path;
        }
    }
}
