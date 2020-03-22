using Marvin.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Marvin.Json.Tests.Helpers
{
    public class JsonPatchDocumentEqualityComparer : IEqualityComparer<IJsonPatchDocument>
    {
        public bool Equals(IJsonPatchDocument x, IJsonPatchDocument y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x == null || y == null)
                return false;

            return Enumerable.SequenceEqual(x.GetOperations(), y.GetOperations(), new OperationEqualityComparer());
        }

        public int GetHashCode(IJsonPatchDocument obj)
        {
            throw new NotImplementedException();
        }
    }
}
