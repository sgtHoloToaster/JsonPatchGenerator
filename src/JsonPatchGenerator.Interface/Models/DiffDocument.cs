using JsonPatchGenerator.Interface.Services;
using System.Collections.Generic;

namespace JsonPatchGenerator.Interface.Models
{
    public class DiffDocument
    {
        public IEnumerable<Operation> Operations { get; set; }

        public DiffDocument() { }

        public DiffDocument(IEnumerable<Operation> operations)
        {
            Operations = operations;
        }

        public string ToJsonPatch(ISerializer serializer) =>
            serializer.Serialize(this);
    }
}
