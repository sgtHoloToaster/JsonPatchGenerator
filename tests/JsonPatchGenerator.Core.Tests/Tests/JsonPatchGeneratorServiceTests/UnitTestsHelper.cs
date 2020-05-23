using AutoMoqCore;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Services;
using Moq;
using System;
using System.Linq;
using OneType.Interface;
using OneType;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public static class UnitTestsHelper
    {
        public static void MockCommonDependencies(AutoMoqer mocker)
        {
            mocker.GetMock<IPatchDocumentBuilderFactory<IPatchDocument>>()
                .Setup(m => m.Create())
                .Returns(() => mocker.Create<IPatchDocumentBuilder<IPatchDocument>>());
            mocker.GetMock<ITypeResolver>()
                .Setup(m => m.GetProperties(It.IsAny<Type>()))
                .Returns<Type>(type => type.GetProperties().Select(p => new DefaultObjectProperty(p.PropertyType, p.Name)).ToList());
            mocker.GetMock<ITypeResolver>()
                .Setup(m => m.GetObjectHashCode(It.IsAny<object>()))
                .Returns<object>(obj => obj.GetHashCode());
            /*mocker.GetMock<ITypeResolver>()
                .Setup(m => m.GetValue(It.IsAny<object>(), It.IsAny<ObjectProperty>()))
                .Returns<object, ObjectProperty>((obj, prop) =>
                {
                    var propertyInfo = obj.GetType().GetProperty(prop.Name);
                    return propertyInfo.GetValue(obj);
                });*/
        }
    }
}
