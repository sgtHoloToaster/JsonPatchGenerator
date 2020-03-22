using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Marvin.JsonPatch.Abstract;
using JsonPatchGenerator.Marvin.JsonPatch.Tests.Helpers;
using JsonPatchGenerator.Marvin.JsonPatch.Tests.Models;
using Marvin.JsonPatch;
using Marvin.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JsonPatchGenerator.Marvin.JsonPatch.Tests.Tests.JsonPatchDocumentGenerator
{
    public class CoreIntegrationTests
    {
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void GeneratedDocumentHasCorrectOperations(JsonPatchDocument expected, Box firstInput, Box secondInput)
        {
            // arrange
            var corePatchGenerator = new JsonPatchGeneratorService<IJsonPatchDocumentWrapper>(new DefaultTypeResolver(), new JsonPatchDocumentBuilderFactory());
            var target = new JsonPatchDocumentGeneratorService(corePatchGenerator);

            // act
            var result = target.Generate(firstInput, secondInput);

            // assert
            Assert.Equal(expected, result, new JsonPatchDocumentEqualityComparer());
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] {
                GetJsonPatchDocument(new Operation("replace", "/Id", null, 2)),
                new Box { Id = 1 },
                new Box { Id = 2 }
            };
            yield return new object[]
            {
                GetJsonPatchDocument(new Operation("replace", "/Title", null, "NewBox"), new Operation("replace", "/Id", null, 42)),
                new Box { Id = 1, Title = "OldBox"},
                new Box { Id = 42, Title = "NewBox" }
            };
        }

        private static JsonPatchDocument GetJsonPatchDocument(params Operation[] operations) =>
            new JsonPatchDocument(operations.ToList(), new DefaultContractResolver());
    }
}
