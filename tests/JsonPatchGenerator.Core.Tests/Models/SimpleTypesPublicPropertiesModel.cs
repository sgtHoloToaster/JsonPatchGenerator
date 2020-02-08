using System;
using System.Collections.Generic;

namespace JsonPatchGenerator.Core.Tests.Models
{
    [Serializable]
    public class SimpleTypesPublicPropertiesModel
    {
        public bool BoolProperty { get; set; }

        public byte ByteProperty { get; set; }

        public sbyte SbyteProperty { get; set; }

        public short ShortProperty { get; set; }

        public ushort UshortProperty { get; set; }

        public int IntProperty { get; set; }

        public uint UintProperty { get; set; }

        public long LongProperty { get; set; }

        public ulong UlongProperty { get; set; }

        public float FloatProperty { get; set; }

        public double DoubleProperty { get; set; }

        public decimal DecimalPropety { get; set; }

        public char CharProperty { get; set; }

        public string StringProperty { get; set; }

        public object ObjectProperty { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SimpleTypesPublicPropertiesModel model &&
                   BoolProperty == model.BoolProperty &&
                   ByteProperty == model.ByteProperty &&
                   SbyteProperty == model.SbyteProperty &&
                   ShortProperty == model.ShortProperty &&
                   UshortProperty == model.UshortProperty &&
                   IntProperty == model.IntProperty &&
                   UintProperty == model.UintProperty &&
                   LongProperty == model.LongProperty &&
                   UlongProperty == model.UlongProperty &&
                   FloatProperty == model.FloatProperty &&
                   DoubleProperty == model.DoubleProperty &&
                   DecimalPropety == model.DecimalPropety &&
                   CharProperty == model.CharProperty &&
                   StringProperty == model.StringProperty &&
                   EqualityComparer<object>.Default.Equals(ObjectProperty, model.ObjectProperty);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(BoolProperty);
            hash.Add(ByteProperty);
            hash.Add(SbyteProperty);
            hash.Add(ShortProperty);
            hash.Add(UshortProperty);
            hash.Add(IntProperty);
            hash.Add(UintProperty);
            hash.Add(LongProperty);
            hash.Add(UlongProperty);
            hash.Add(FloatProperty);
            hash.Add(DoubleProperty);
            hash.Add(DecimalPropety);
            hash.Add(CharProperty);
            hash.Add(StringProperty);
            hash.Add(ObjectProperty);
            return hash.ToHashCode();
        }
    }
}
