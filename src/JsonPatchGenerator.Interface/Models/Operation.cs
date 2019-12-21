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
    }
}
