using System;

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
    }
}
