using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Interface.Enums;
using JsonPatchGenerator.JsonNet.Models;
using JsonPatchGenerator.JsonNet.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.JsonNet.Serializer.Service
{
    public class JsonNetSerializer : ISerializer
    {
        readonly OperationsFactory _operationsFactory = new OperationsFactory();
        readonly IPatchDocumentBuilderFactory<IPatchDocument> _patchDocumentBuilderFactory;

        public JsonNetSerializer(IPatchDocumentBuilderFactory<IPatchDocument> patchDocumentBuilderFactory)
        {
            _patchDocumentBuilderFactory = patchDocumentBuilderFactory;
        }

        public string Serialize(IPatchDocument diff)
        {
            var operations = diff.Operations.Select(o => _operationsFactory.FromExternalModel(o));
            return JsonConvert.SerializeObject(operations);
        }

        public IPatchDocument Deserialize(string json)
        {
            var builder = _patchDocumentBuilderFactory.Create();
            JsonConvert.DeserializeObject<List<OperationFullModel>>(json)
                .ForEach(o => builder.AppendOperation((OperationType)(int)o.Type, o.Path, o.Value, o.From));
            return builder.Build();
        }
    }
}
