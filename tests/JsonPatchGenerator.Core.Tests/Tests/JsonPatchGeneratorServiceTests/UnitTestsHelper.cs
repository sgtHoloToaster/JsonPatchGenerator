using AutoMoqCore;
using JsonPatchGenerator.Core.Models;
using JsonPatchGenerator.Interface.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public static class UnitTestsHelper
    {
        public static void MockCommonDependencies(AutoMoqer mocker)
        {
            mocker.GetMock<IPatchDocumentBuilderFactory>()
                .Setup(m => m.Create())
                .Returns(() => mocker.Create<IPatchDocumentBuilder>());
            mocker.GetMock<ITypeResolver>()
                .Setup(m => m.GetProperties(It.IsAny<Type>()))
                .Returns<Type>(type => type.GetProperties().Select(p => new ObjectProperty(p)).ToList());
            mocker.GetMock<ITypeResolver>()
                .Setup(m => m.GetHashCode(It.IsAny<object>()))
                .Returns<object>(obj => obj.GetHashCode());
            mocker.GetMock<ITypeResolver>()
                .Setup(m => m.GetValue(It.IsAny<object>(), It.IsAny<ObjectProperty>()))
                .Returns<object, ObjectProperty>((obj, prop) =>
                {
                    var propertyInfo = obj.GetType().GetProperty(prop.Name);
                    return propertyInfo.GetValue(obj);
                });
        }
    }
}
