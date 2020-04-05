namespace JsonPatchGenerator.Interface.Services
{
    public interface IJsonPatchGeneratorGeneric<T> : IJsonPatchGenerator<T>
    {
        T Generate<T1>(T1 first, T1 second);
    }
}
