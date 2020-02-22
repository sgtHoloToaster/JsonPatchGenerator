using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Collections.Generic;

namespace JsonPatchGenerator.AspNetCore.Tests.Helpers
{
    public class OperationEqualityComparer : IEqualityComparer<Operation>
    {
        public bool Equals(Operation x, Operation y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x == null || y == null)
                return false;

            return Equals(x.from, y.from)
                && Equals(x.op, y.op)
                && Equals(x.path, y.path)
                && Equals(x.value, y.value);
        }

        public int GetHashCode(Operation obj)
        {
            throw new NotImplementedException();
        }
    }
}
