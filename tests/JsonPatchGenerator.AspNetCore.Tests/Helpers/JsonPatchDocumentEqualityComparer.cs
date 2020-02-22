using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JsonPatchGenerator.AspNetCore.Tests.Helpers
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

        public int GetHashCode([DisallowNull] IJsonPatchDocument obj)
        {
            throw new NotImplementedException();
        }
    }
}
