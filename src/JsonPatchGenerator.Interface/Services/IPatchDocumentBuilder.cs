using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Enums;

namespace JsonPatchGenerator.Interface.Services
{
    public interface IPatchDocumentBuilder<out TPatchDocument> where TPatchDocument : IPatchDocument
    {
        IPatchDocumentBuilder<TPatchDocument> AppendAddOperation<T>(string path, T value);

        IPatchDocumentBuilder<TPatchDocument> AppendRemoveOperation(string path);

        IPatchDocumentBuilder<TPatchDocument> AppendTestOperation<T>(string path, T value);

        IPatchDocumentBuilder<TPatchDocument> AppendCopyOperation(string path, string from);

        IPatchDocumentBuilder<TPatchDocument> AppendMoveOperation(string path, string from);

        IPatchDocumentBuilder<TPatchDocument> AppendReplaceOperation<T>(string path, T value);

        IPatchDocumentBuilder<TPatchDocument> AppendOperation<T>(OperationType operationType, string path, T value, string from);

        IPatchDocument Build();
    }
}
