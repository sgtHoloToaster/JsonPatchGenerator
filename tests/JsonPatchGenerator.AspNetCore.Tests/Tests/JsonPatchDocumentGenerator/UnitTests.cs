using AutoMoqCore;
using JsonPatchGenerator.AspNetCore.Abstract;
using JsonPatchGenerator.AspNetCore.Tests.Models;
using JsonPatchGenerator.Interface.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using Xunit;

namespace JsonPatchGenerator.AspNetCore.Tests.Tests.JsonPatchDocumentGenerator
{
    public class UnitTests
    {
        readonly AutoMoqer _mocker = new AutoMoqer();

        [Fact]
        public void CanGenerateDocument()
        {
            // arrange
            var first = new object();
            var second = new object();
            var expected = new JsonPatchDocument();
            _mocker.GetMock<IJsonPatchGenerator<IJsonPatchDocumentWrapper>>()
                .Setup(m => m.Generate(first, second))
                .Returns(new JsonPatchDocumentWrapper(expected));
            var target = _mocker.Create<JsonPatchDocumentGeneratorService>();

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
            _mocker.GetMock<IJsonPatchGenerator<IJsonPatchDocumentWrapper>>()
                .Setup(m => m.Generate(first, second))
                .Returns(new JsonPatchDocumentWrapper(expected));
            var target = _mocker.Create<JsonPatchDocumentGeneratorService>();

            // act
            var result = target.Generate(first, second);

            // assert
            Assert.Equal(expected, result);
        }
    }
}
