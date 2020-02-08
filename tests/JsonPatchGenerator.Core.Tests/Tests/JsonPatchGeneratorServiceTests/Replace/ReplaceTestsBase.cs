﻿using JsonPatchGenerator.Core.Services;
using JsonPatchGenerator.Core.Tests.Helpers;
using JsonPatchGenerator.Core.Tests.Models;
using JsonPatchGenerator.Interface.Models.Abstract;
using JsonPatchGenerator.Interface.Enums;
using System.Linq;
using Xunit;
using static JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests.BaseTestsHelper;
using System;

namespace JsonPatchGenerator.Core.Tests.Tests.JsonPatchGeneratorServiceTests
{
    public class ReplaceTestsBase
    {
        readonly Func<JsonPatchGeneratorService> _getTarget;

        public ReplaceTestsBase(Func<JsonPatchGeneratorService> getTarget) 
        {
            _getTarget = getTarget;
        }

        public void SupportReplaceOperationForSimpleTypes() =>
            TestReplaceSimpleTypeOperation(HasReplaceOperation);

        public void ReplaceOperationHasCorrectValue() =>
            TestReplaceSimpleTypeOperation(HasCorrectValue);

        public void ReplaceOperationHasCorrectPath() =>
            TestReplaceSimpleTypeOperation(HasCorrectPath);

        public void DontCreateExtraOperationsOnReplace() =>
            TestReplaceSimpleTypeOperation(HasNoExtraOperations);

        private void TestReplaceSimpleTypeOperation(AssertAction assert)
        {
            // arrange
            var first = new SimpleTypesPublicPropertiesModel();
            var second = new SimpleTypesPublicPropertiesModel();
            var propertyName = nameof(SimpleTypesPublicPropertiesModel.IntProperty);
            var property = first.GetType().GetProperty(propertyName);
            const int value = 42;
            property.SetValue(first, value);
            var path = $"/{propertyName}";
            var changedValue = value + 1;
            property.SetValue(second, changedValue);
            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            assert(result, path, changedValue);
        }

        public void SupportReplaceOperationsForNestedObjects() =>
            TestComplexTypeNestedPropertiesReplace(HasReplaceOperation);

        public void CreateCorrectPathForNestedPropertiesOnReplace() =>
            TestComplexTypeNestedPropertiesReplace(HasCorrectPath);

        private void TestComplexTypeNestedPropertiesReplace(AssertAction assert)
        {
            // arrange
            var complexTypePropertyName = nameof(ComplexPropertiesModel.ComplexTypeProperty);
            var simpleTypePropertyName = nameof(ComplexPropertiesModel.SimpleTypeProperty);
            var path = $"/{complexTypePropertyName}/{complexTypePropertyName}/{complexTypePropertyName}/{simpleTypePropertyName}";
            const int initValue = 441;
            var first = new ComplexPropertiesModel();
            PropertiesPathfinder.SetValue(first, path, initValue);
            var second = new ComplexPropertiesModel();
            var newValue = initValue + 551;
            PropertiesPathfinder.SetValue(second, path, newValue);
            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            assert(result, path, newValue);
        }

        public void ReplaceOperationForComplexPropertiesHasCorrectValue()
        {
            // arrange
            var first = new ComplexPropertiesModel();
            var value = new ComplexPropertiesModel { SimpleTypeProperty = 123 };
            var second = new ComplexPropertiesModel { ComplexTypeProperty = value };
            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            HasCorrectValue(result, value);
        }

        public void SupportReplacingValuesWithNull()
        {
            // arrange
            var first = new ComplexPropertiesModel { ComplexTypeProperty = new ComplexPropertiesModel { SimpleTypeProperty = 777 } };
            var second = new ComplexPropertiesModel();
            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            HasReplaceOperation(result);
        }

        public void SupportSimpleTypeArrayElementReplacing() =>
            TestSimpleTypeArrayElementReplacing(HasReplaceOperation);

        public void SimpleTypeArrayElementReplaceOperationHasCorrectValue() =>
            TestSimpleTypeArrayElementReplacing(HasCorrectValue);

        public void ArrayElementReplaceOperationHasCorrectPath() =>
            TestSimpleTypeArrayElementReplacing(HasCorrectPath);

        private void TestSimpleTypeArrayElementReplacing(AssertAction assert)
        {
            // arrange
            var initialArray = new int[] { 1, 2, 3 };
            const int newValue = 7;
            const int changedValueIndex = 1;
            var first = new ComplexPropertiesModel { SimpleTypeArray = initialArray.Clone() as int[] };
            var second = new ComplexPropertiesModel { SimpleTypeArray = initialArray.Clone() as int[] };
            var changedValuePath = $"/{nameof(ComplexPropertiesModel.SimpleTypeArray)}/{changedValueIndex}";
            PropertiesPathfinder.SetValue(second, changedValuePath, newValue);
            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            assert(result, changedValuePath, newValue);
        }

        public void SupportReplacingPropertiesOfArrayElement() =>
            TestComplexTypeArrayElementPropertyReplace(HasReplaceOperation);

        public void ArrayElementPropertyReplaceOperationHasCorrectValue() =>
            TestComplexTypeArrayElementPropertyReplace(HasCorrectValue);

        public void ArrayElementPropertyReplaceOperationHasCorrectPath() =>
            TestComplexTypeArrayElementPropertyReplace(HasCorrectPath);

        private void TestComplexTypeArrayElementPropertyReplace(AssertAction assert)
        {
            // arrange
            var initialModel = new ComplexPropertiesModel
            {
                ComplexTypeArrayProperty = new ComplexPropertiesModel[]
                {
                    new ComplexPropertiesModel(),
                    new ComplexPropertiesModel { SimpleTypeProperty = 55 },
                    new ComplexPropertiesModel()
                }
            };

            var first = initialModel;
            var second = ObjectCloner.DeepClone(initialModel);
            const int newValue = 77;
            const int changedValueIndex = 1;
            var changedValuePath = $"/{nameof(ComplexPropertiesModel.ComplexTypeArrayProperty)}/{changedValueIndex}/{nameof(ComplexPropertiesModel.SimpleTypeProperty)}";
            PropertiesPathfinder.SetValue(second, changedValuePath, newValue);
            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            assert(result, changedValuePath, newValue);
        }

        public void CanHandleMultipleDifferences()
        {
            // arrange
            var first = new SimpleTypesPublicPropertiesModel();
            var second = new SimpleTypesPublicPropertiesModel
            {
                BoolProperty = true,
                ByteProperty = 1,
                IntProperty = 13
            };

            var target = _getTarget();

            // act
            var result = target.Generate(first, second);

            // assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Operations.Count());
        }

        private void HasReplaceOperation(IPatchDocument result) =>
            HasOperation(result, OperationType.Replace);

        private void HasReplaceOperation(IPatchDocument result, string path, object newValue) =>
            HasReplaceOperation(result);
    }
}
