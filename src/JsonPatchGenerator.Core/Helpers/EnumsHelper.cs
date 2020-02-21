using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace JsonPatchGenerator.Core.Helpers
{
    public static class EnumsHelper
    {
        public static T GetValueByEnumMemberAttribute<T>(string attributeValue) where T : Enum
        {
            var type = typeof(T);
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute)
                {
                    if (Equals(attribute.Value, attributeValue))
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found", nameof(attributeValue));
        }

        public static string GetEnumMemberAttributeValue<T>(T enumValue) where T : Enum
        {
            var type = typeof(T);
            var memberInfo = type.GetMember(enumValue.ToString())[0];
            var attribute = Attribute.GetCustomAttribute(memberInfo, typeof(EnumMemberAttribute)) as EnumMemberAttribute;
            return attribute.Value;
        }
    }
}
