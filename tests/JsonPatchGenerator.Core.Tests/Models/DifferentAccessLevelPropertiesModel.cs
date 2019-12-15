namespace JsonPatchGenerator.Core.Tests.Models
{
    public class DifferentAccessLevelPropertiesModel
    {
        public int PublicProperty { get; set; }

        protected int ProtectedProperty { get; set; }

        internal int InternalProperty { get; set; }

        private int PrivateProperty { get; set; }
    }
}
