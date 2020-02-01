using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.JsonNet.Enums;
using Newtonsoft.Json;

namespace JsonPatchGenerator.JsonNet.Models
{
    internal class OperationWithFromModel : OperationBaseModel
    {
        public OperationWithFromModel() { }

        public OperationWithFromModel(Operation operation) : base(operation)
        {
            From = operation.From;
        }

        [JsonProperty(PropertyName = "from", NullValueHandling = NullValueHandling.Ignore)]
        public string From { get; set; }
    }
}
