using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.JsonNet.Enums;
using JsonPatchGenerator.JsonNet.Models;
using System;

namespace JsonPatchGenerator.JsonNet.Service
{
    internal class OperationsFactory
    {
        public OperationBaseModel FromExternalModel(Operation operation)
        {
            switch (operation.Type)
            {
                case OperationType.Add:
                case OperationType.Replace:
                case OperationType.Test:
                    return new OperationFullModel(operation);
                case OperationType.Copy:
                case OperationType.Move:
                    return new OperationWithFromModel(operation);
                case OperationType.Remove:
                    return new OperationBaseModel(operation);
                default:
                    throw new InvalidOperationException("The operation type is not supported!");
            }
        }
    }
}
