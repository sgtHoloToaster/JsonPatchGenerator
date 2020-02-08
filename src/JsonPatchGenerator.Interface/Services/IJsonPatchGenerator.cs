namespace JsonPatchGenerator.Interface.Services
{
    public interface IJsonPatchGenerator<T>
    {
        T Generate(object first, object second);
    }
}
