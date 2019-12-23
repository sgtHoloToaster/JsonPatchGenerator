using AutoMoqCore;
using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Interface.Services;
using Moq;
using System;
using System.Linq;

namespace JsonPatchGenerator.Core.Tests.Tests
{
    public class JsonPatchGeneratorTests
    {
        protected readonly AutoMoqer Mocker = new AutoMoqer();

        public JsonPatchGeneratorTests()
        {
            Mocker.SetInstance<ITypeResolver>(new DefaultTypeResolver());
        }
    }
}
