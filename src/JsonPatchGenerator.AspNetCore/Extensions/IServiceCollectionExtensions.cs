using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.DependencyInjection;
using OneType;
using OneType.Interface;

namespace JsonPatchGenerator.AspNetCore.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddJsonPatchGenerator(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddScoped<ITypeResolver, DefaultTypeResolver>()
                .AddScoped<IPatchDocumentBuilderFactory<IJsonPatchDocumentWrapper>, JsonPatchDocumentBuilderFactory>()
                .AddScoped<IJsonPatchGenerator<IJsonPatchDocumentWrapper>, JsonPatchGeneratorService<IJsonPatchDocumentWrapper>>()
                .AddScoped<IJsonPatchGenerator<IJsonPatchDocument>, JsonPatchDocumentGenerator>();
    }
}
