using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.JsonNet.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Json.NET.Serializer.Service
{
    public class JsonNetSerializer : ISerializer
    {
        public string Serialize(DiffDocument diff)
        {
            var operations = diff.Operations.Select(o => new OperationModel(o));
            return JsonConvert.SerializeObject(operations);
        }

        public DiffDocument Deserialize(string json)
        {
            var operations = JsonConvert.DeserializeObject<IEnumerable<OperationModel>>(json)
                .Select(o => (Operation)o)
                .ToList();

            return new DiffDocument(operations);
        }
    }
}
