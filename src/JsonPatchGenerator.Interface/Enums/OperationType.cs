using System.Runtime.Serialization;

namespace JsonPatchGenerator.Interface.Enums
{
    public enum OperationType
    {
        [EnumMember(Value = "add")]
        Add     = 0,

        [EnumMember(Value = "remove")]
        Remove  = 1,

        [EnumMember(Value = "replace")]
        Replace = 2,

        [EnumMember(Value = "move")]
        Move    = 3,

        [EnumMember(Value = "copy")]
        Copy    = 4,

        [EnumMember(Value = "test")]
        Test    = 5
    }
}
