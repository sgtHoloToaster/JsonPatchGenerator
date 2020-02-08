namespace JsonPatchGenerator.Core.Tests.Models
{
    public class DifferentAccessLevelPropertiesModel
    {
        public int PublicProperty { get; set; }

        protected int ProtectedProperty { get; set; }

        internal int InternalProperty { get; set; }

        private int PrivateProperty { get; set; }

        public override int GetHashCode()
        {
            var hash = 17;
            unchecked
            {
                hash *= 23 + PublicProperty.GetHashCode();
                hash *= 23 + ProtectedProperty.GetHashCode();
                hash *= 23 + InternalProperty.GetHashCode();
                hash *= 23 + PrivateProperty.GetHashCode();
            }

            return hash;
        }
    }
}
