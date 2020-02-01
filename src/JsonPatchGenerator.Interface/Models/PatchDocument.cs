using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Interface.Models
{
    public class PatchDocument : IPatchDocument
    {
        public IEnumerable<Operation> Operations { get; }

        public PatchDocument() { }

        public PatchDocument(IEnumerable<Operation> operations)
        {
            Operations = operations;
        }

        public string Serialize(ISerializer serializer) =>
            serializer.Serialize(this);

        public override bool Equals(object obj) =>
            Equals(obj as IPatchDocument);

        public bool Equals(IPatchDocument diffDocument)
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
