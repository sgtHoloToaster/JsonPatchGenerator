using JsonPatchGenerator.Interface.Services;
using System.Collections.Generic;
using System.Linq;

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

        public override bool Equals(object obj) =>
            Equals(obj as DiffDocument);

        public bool Equals(DiffDocument diffDocument)
        {
            if (diffDocument == null)
                return false;

            if (ReferenceEquals(this, diffDocument))
                return true;

            return Enumerable.SequenceEqual(Operations, diffDocument.Operations);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            unchecked
            {
                foreach (var operation in Operations)
                    hash *= 23 + operation.GetHashCode();
            }

            return hash;
        }
    }
}
