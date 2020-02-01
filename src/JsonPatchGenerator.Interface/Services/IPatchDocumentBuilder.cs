using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.JsonNet.Enums;

namespace JsonPatchGenerator.Interface.Services
{
    public interface IPatchDocumentBuilder
    {
        IPatchDocumentBuilder AppendAddOperation<T>(string path, T value);

        IPatchDocumentBuilder AppendRemoveOperation(string path);

        IPatchDocumentBuilder AppendTestOperation<T>(string path, T value);

        IPatchDocumentBuilder AppendCopyOperation(string path, string from);

        IPatchDocumentBuilder AppendMoveOperation(string path, string from);

        IPatchDocumentBuilder AppendReplaceOperation<T>(string path, T value);

        IPatchDocumentBuilder AppendOperation(OperationType operationType, string path, object value, string from);

        IPatchDocumentBuilder AppendOperation<T>(OperationType operationType, string path, T value, string from);

        IPatchDocument Build();
    }
}
