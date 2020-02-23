using System;
using System.Collections.Generic;

namespace JsonPatchGenerator.Core.Tests.Models
{
    [Serializable]
    public class ComplexPropertiesModel
    {
        public ComplexPropertiesModel ComplexTypeProperty { get; set; }

        public int[] SimpleTypeArray { get; set; }

        public ComplexPropertiesModel[] ComplexTypeArrayProperty { get; set; }

        public IList<int> SimpleTypeList { get; set; }

        public int SimpleTypeProperty { get; set; }

        public override int GetHashCode()
        {
            var hash = 17;
            unchecked
            {
                hash *= 23 + (ComplexTypeProperty?.GetHashCode() ?? 0);
                hash *= 23 + (SimpleTypeArray?.GetHashCode() ?? 0);
                hash *= 23 + (ComplexTypeArrayProperty?.GetHashCode() ?? 0);
                hash *= 23 + (SimpleTypeProperty.GetHashCode());
            }

            return hash;
        }

        public bool Equals(ComplexPropertiesModel obj)
        {
            if (obj == null)
                return false;

            return ReferenceEquals(this, obj) ||
                Equals(ComplexTypeProperty, obj.ComplexTypeProperty) &&
                Equals(SimpleTypeArray, obj.SimpleTypeArray) &&
                Equals(ComplexTypeProperty, obj.ComplexTypeProperty) &&
                Equals(SimpleTypeProperty, obj.SimpleTypeProperty);
        }

        public override bool Equals(object obj) =>
            Equals(obj as ComplexPropertiesModel);
    }
}
