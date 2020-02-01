using JsonPatchGenerator.JsonNet.Enums;

namespace JsonPatchGenerator.Interface.Models
{
    public class Operation
    {
        public OperationType Type { get; set;  }

        public string From { get; set;  }

        public object Value { get; set;  }

        public string Path { get; set;  }

        public Operation(OperationType type, object value, string path) : this(type, value, path, null) { }

        public Operation(OperationType type, object value, string path, string from)
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
    }
}
