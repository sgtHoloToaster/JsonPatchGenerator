using JsonPatchGenerator.Core.Helpers;
using JsonPatchGenerator.Interface.Enums;
using System;
using Xunit;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class EnumsHelperTests
    {
        [Theory]
        [InlineData("add", OperationType.Add)]
        [InlineData("remove", OperationType.Remove)]
        [InlineData("test", OperationType.Test)]
        public void ReturnsEnumValueForEnumMemberAttributeValue(string input, OperationType expected)
        {
            // act
            var result = EnumsHelper.GetValueByEnumMemberAttribute<OperationType>(input);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ThrowsArgumentExceptionWhenUnknownValueIsPassed()
        {
            // arrange
            const string input = "someEnumMemberThatDoesNotForSureExist";

            // act & assert
            Assert.Throws<ArgumentException>(() => EnumsHelper.GetValueByEnumMemberAttribute<OperationType>(input));
        }
    }
}
