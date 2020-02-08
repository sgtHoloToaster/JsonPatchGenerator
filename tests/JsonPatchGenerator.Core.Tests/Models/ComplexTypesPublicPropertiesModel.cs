using System.Collections.Generic;

namespace JsonPatchGenerator.Core.Tests.Models
{
    public class ComplexTypesPublicPropertiesModel
    {
        public int[] ArrayProperty { get; set; }

        public IEnumerable<int> EnumerableProperty { get; set; }

        public SimpleTypesPublicPropertiesModel CustomTypeProperty { get; set; }

        public override int GetHashCode()
        {
            var hash = 17;
            unchecked
            {
                hash *= 23 + (ArrayProperty?.GetHashCode() ?? 0);
                hash *= 23 + (EnumerableProperty?.GetHashCode() ?? 0);
                hash *= 23 + (CustomTypeProperty?.GetHashCode() ?? 0);
            }

            return hash;
        }
    }
}
