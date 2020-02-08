using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace JsonPatchGenerator.AspNetCore
{
    public class JsonPatchGeneratorService : IJsonPatchGenerator<IJsonPatchDocument>
    {
        public IJsonPatchDocument Generate(object first, object second)
        {
            throw new NotImplementedException();
        }
    }
}
