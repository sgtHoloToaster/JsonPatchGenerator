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
            /*Mocker.GetMock<ITypeResolver>()
               .Setup(m => m.GetProperties(It.IsAny<Type>()))
               .Returns<Type>(t => t.GetProperties().Select(p => new ObjectProperty(p)));

            Mocker.GetMock<ITypeResolver>()
                   .Setup(m => m.GetValue(It.IsAny<object>(), It.IsAny<ObjectProperty>()))
                   .Returns<object, ObjectProperty>((obj, prop) => obj.GetType().GetProperty(prop.Name).GetValue(obj));

            Mocker.GetMock<ITypeResolver>()
                .Setup(m => m.GetHashCode(It.IsAny<int>()))
                .Returns((int value) => value);*/
            Mocker.SetInstance<ITypeResolver>(new DefaultTypeResolver());
        }
    }
}
