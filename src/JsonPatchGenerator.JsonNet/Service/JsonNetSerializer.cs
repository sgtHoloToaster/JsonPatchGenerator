using JsonPatchGenerator.Interface.Models;
using JsonPatchGenerator.Interface.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonPatchGenerator.Json.NET.Serializer.Service
{
    public class JsonNetSerializer : ISerializer
    {
        public string Serialize(DiffDocument diff)
        {
            throw new NotImplementedException();
        }

        public DiffDocument Deserialize(string json)
        {
            throw new NotImplementedException();
        }
    }
}
