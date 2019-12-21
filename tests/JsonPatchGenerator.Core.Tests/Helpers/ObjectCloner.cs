using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace JsonPatchGenerator.Core.Tests.Helpers
{
    public static class ObjectCloner
    {
        public static T DeepClone<T>(T obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, obj);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(memoryStream);
            }
        }
    }
}
