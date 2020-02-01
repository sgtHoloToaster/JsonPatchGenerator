using JsonPatchGenerator.Interface.Enums;

namespace JsonPatchGenerator.Interface.Models
{
    public class Operation
    {
        public OperationType Type { get; set;  }

        public string From { get; set;  }

        public object Value { get; set;  }

        public string Path { get; set;  }

        public Operation(OperationType type, string path) : this(type, path, null, null) { }

        public Operation(OperationType type, string path, object value) : this(type, path, value, null) { }

        public Operation(OperationType type, string path, object value, string from)
        {
            Type = type;
            Value = value;
            Path = path;
            From = from;
        }

        public override bool Equals(object obj) =>
            Equals(obj as Operation);

        public bool Equals(Operation obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return Equals(Type, obj.Type)
                && Equals(Value, obj.Value)
                && Equals(Path, obj.Path)
                && Equals(From, obj.From);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            unchecked
            {
                hash *= 23 + Type.GetHashCode();
                hash *= 23 + (Value?.GetHashCode() ?? int.MinValue);
                hash *= 23 + (Path?.GetHashCode() ?? 0);
                hash *= 23 + (From?.GetHashCode() ?? 0);
            }

            return hash;
        }
    }
}
