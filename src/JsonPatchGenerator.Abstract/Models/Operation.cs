namespace JsonPatchGenerator.Interface.Models
{
    public class Operation
    {
        public OperationType Type { get; set;  }

        public string From { get; set;  }

        public object Value { get; set;  }

        public string Path { get; set;  }
    }
}
