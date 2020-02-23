using JsonPatchGenerator.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonPatchGenerator.Core.Tests.Models
{
    [Serializable]
    public class ComplexPropertiesModel
    {
        public ComplexPropertiesModel ComplexType { get; set; }

        public int[] SimpleTypeArray { get; set; }

        public ComplexPropertiesModel[] ComplexTypeArray { get; set; }

        public IList<int> SimpleTypeList { get; set; }

        public IList<ComplexPropertiesModel> ComplexTypeList { get; set; }

        public int SimpleType { get; set; }

        public override int GetHashCode()
        {
            var hash = 17;
            unchecked
            {
                hash *= 23 + (ComplexType?.GetHashCode() ?? 0);
                hash *= 23 + (SimpleTypeArray?.GetElementsHashCode() ?? 0);
                hash *= 23 + (ComplexTypeArray?.GetElementsHashCode() ?? 0);
                hash *= 23 + SimpleType.GetHashCode();
                hash *= 23 + (SimpleTypeList?.GetElementsHashCode() ?? 0);
                hash *= 23 + (ComplexTypeList?.GetElementsHashCode() ?? 0);
            }

            return hash;
        }

        public bool Equals(ComplexPropertiesModel obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return Equals(ComplexType, obj.ComplexType)
                && Enumerable.SequenceEqual(SimpleTypeArray, obj.SimpleTypeArray)
                && Equals(ComplexType, obj.ComplexType)
                && Equals(SimpleType, obj.SimpleType)
                && Enumerable.SequenceEqual(SimpleTypeList, obj.SimpleTypeList)
                && Enumerable.SequenceEqual(ComplexTypeList, obj.ComplexTypeList);
        }

        public override bool Equals(object obj) =>
            Equals(obj as ComplexPropertiesModel);
    }
}
