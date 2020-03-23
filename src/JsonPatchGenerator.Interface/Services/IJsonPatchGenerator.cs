namespace JsonPatchGenerator.Interface.Services
{
    public interface IJsonPatchGenerator<T>
    {
        T Generate(object first, object second);

        T Generate<T1>(T1 first, T1 second);
    }
}
