﻿using AutoMoq;
using JsonPatchGenerator.Interface.Services;
using JsonPatchGenerator.Marvin.JsonPatch;
using JsonPatchGenerator.Marvin.JsonPatch.Abstract;
using JsonPatchGenerator.Marvin.JsonPatch.Tests.Models;
using Marvin.JsonPatch;
using Marvin.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using Xunit;

namespace JsonPatchGenerator.Marvin.Json.Tests.Tests.JsonPatchDocumentGenerator
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
            _mocker.GetMock<IJsonPatchGenerator<IJsonPatchDocumentWrapper>>()
                .Setup(m => m.Generate(first, second))
                .Returns(new JsonPatchDocumentWrapper(expected));
            var target = _mocker.Create<JsonPatch.JsonPatchDocumentGenerator>();

            // act
            var result = target.Generate(first, second);

            // assert
            Assert.Equal(expected, result);
        }
    }
}
