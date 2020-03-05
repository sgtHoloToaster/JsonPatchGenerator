using JsonPatchGenerator.AspNetCore.Services;
using JsonPatchGenerator.AspNetCore.Services.Abstract;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.DependencyInjection;

namespace JsonPatchGenerator.AspNetCore.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddJsonPatchGenerator(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddScoped<ITypeResolver, DefaultTypeResolver>()
                .AddScoped<IPatchDocumentBuilderFactory<IJsonPatchDocumentWrapper>, JsonPatchDocumentBuilderFactory>()
                .AddScoped<IJsonPatchGenerator<IJsonPatchDocumentWrapper>, JsonPatchGeneratorService<IJsonPatchDocumentWrapper>>()
                .AddScoped<IJsonPatchGenerator<IJsonPatchDocument>, JsonPatchDocumentGeneratorService>();
    }
}
