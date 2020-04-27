using AutoMoq;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Marvin.JsonPatch.Abstract;
using JsonPatchGenerator.Marvin.JsonPatch.Tests.Models;
using Marvin.JsonPatch;
using Marvin.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using Xunit;

namespace JsonPatchGenerator.Marvin.JsonPatch.Tests.Tests.JsonPatchDocumentGenerator
{
    public class UnitTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();

        [Fact]
        public void CanGenerateDocument()
        {
            // arrange
            var first = new Box();
            var second = new Box();
            var expected = new JsonPatchDocument();
            _mocker.GetMock<IJsonPatchGeneratorGeneric<IJsonPatchDocumentWrapper>>()
                .Setup(m => m.Generate(first, second))
                .Returns(new JsonPatchDocumentWrapper(expected));
            var target = _mocker.Create<JsonPatch.JsonPatchDocumentGenerator>();

            // act
            var result = target.Generate(first, second);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GeneratedDocumentHasCorrectOperations()
        {
            // arrange 
            var first = new Box { Id = 1, Title = "OldBox" };
            var second = new Box { Id = 2, Title = "NewBox" };
            var expectedOperations = new List<Operation>
            {
                new Operation("replace", "/Title", null, "NewBox"),
                new Operation("replace", "/Id", null, 2)
            };
            var expected = new JsonPatchDocument(expectedOperations, new DefaultContractResolver());
            _mocker.GetMock<IJsonPatchGeneratorGeneric<IJsonPatchDocumentWrapper>>()
                .Setup(m => m.Generate(first, second))
                .Returns(new JsonPatchDocumentWrapper(expected));
            var target = _mocker.Create<JsonPatch.JsonPatchDocumentGenerator>();

            // act
            var result = target.Generate(first, second);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GeneratedGenericDocumentHasCorrectType()
        {
            // arrange 
            var first = new Box { Id = 1, Title = "OldBox" };
            var second = new Box { Id = 2, Title = "NewBox" };
            var expectedOperations = new List<Operation<Box>>
            {
                new Operation<Box>("replace", "/Title", null, "NewBox"),
                new Operation<Box>("replace", "/Id", null, 2)
            };
            var expected = new JsonPatchDocument<Box>(expectedOperations, new DefaultContractResolver());
            _mocker.GetMock<IJsonPatchGeneratorGeneric<IJsonPatchDocumentWrapper>>()
                .Setup(m => m.Generate(first, second))
                .Returns(new JsonPatchDocumentWrapper(expected));
            var target = _mocker.Create<JsonPatch.JsonPatchDocumentGenerator>();

            // act
            var result = target.Generate(first, second);

            // assert
            Assert.True(result is JsonPatchDocument<Box>);
        }
    }
}
