using System.Collections.Generic;

namespace JsonPatchGenerator.Core.Tests.Models
{
    public class ComplexTypesPublicPropertiesModel
    {
        public int[] ArrayProperty { get; set; }

        public IEnumerable<int> EnumerableProperty { get; set; }

        public SimpleTypesPublicPropertiesModel CustomTypeProperty { get; set; }
    }
}
