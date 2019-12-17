namespace JsonPatchGenerator.Core.Tests.Models
{
    public class ComplexPropertiesModel
    {
        public ComplexPropertiesModel ComplexTypeProperty { get; set; }

        public int[] SimpleTypeArray { get; set; }

        public ComplexPropertiesModel[] ComplexTypeArrayProperty { get; set; }

        public int SimpleTypeProperty { get; set; }
    }
}
