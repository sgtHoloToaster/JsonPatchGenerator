using AutoMoqCore;
using JsonPatchGenerator.AspNetCore.Extensions;
using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace JsonPatchGenerator.AspNetCore.Tests.Tests
{
    public class IServiceCollectionExtensionsTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();

        [Fact]
        public void ReturnsTheSameReferenceToServiceCollection()
        {
            // arrange
            var serviceCollection = _mocker.GetMock<IServiceCollection>().Object;

            // act 
            var result = serviceCollection.AddJsonPatchGenerator();

            // assert
            Assert.Same(serviceCollection, result);
        }

        [Fact]
        public void RegistersJsonPatchDocumentGenerator() =>
            TestIfServiceProviderAfterRegisteringJsonPatchGenerator(
                MakeCorrectResolve<IJsonPatchGenerator<IJsonPatchDocument>, JsonPatchDocumentGeneratorService>);

        [Fact]
        public void RegistersInternalJsonPatchDocumentWrapperGenerator() =>
            TestIfServiceProviderAfterRegisteringJsonPatchGenerator(
                MakeCorrectResolve<IJsonPatchGenerator<IJsonPatchDocumentWrapper>, JsonPatchGeneratorService<IJsonPatchDocumentWrapper>>);

        [Fact]
        public void RegistersJsonPatchDocumentBuilderFactory() =>
            TestIfServiceProviderAfterRegisteringJsonPatchGenerator(
                MakeCorrectResolve<IPatchDocumentBuilderFactory<IJsonPatchDocumentWrapper>, JsonPatchDocumentBuilderFactory>);

        [Fact]
        public void RegistersTypeResolver() =>
            TestIfServiceProviderAfterRegisteringJsonPatchGenerator(MakeCorrectResolve<ITypeResolver, DefaultTypeResolver>);

        private void MakeCorrectResolve<TReq, TRes>(ServiceProvider provider) where TRes : TReq =>
            Assert.True(provider.GetRequiredService<TReq>() is TRes);

        private void TestIfServiceProviderAfterRegisteringJsonPatchGenerator(Action<ServiceProvider> assert)
        {
            // arrange
            var serviceCollection = new ServiceCollection();

            // act 
            var result = serviceCollection.AddJsonPatchGenerator()
                .BuildServiceProvider();

            // assert
            assert(result);
        }
    }
}
