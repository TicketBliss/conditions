﻿#region Copyright (c) 2008 S. van Deursen
/* The CuttingEdge.Conditions library enables developers to validate pre- and postconditions in a fluent 
 * manner.
 * 
 * Copyright (C) 2008 S. van Deursen
 * 
 * To contact me, please visit my blog at http://www.cuttingedge.it/blogs/steven/ 
 *
 * This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser 
 * General Public License as published by the Free Software Foundation; either version 2.1 of the License, or
 * (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the 
 * implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public
 * License for more details.
*/
#endregion

using System;
using System.Collections;
using System.Globalization;
using System.Linq.Expressions;

namespace CuttingEdge.Conditions
{
    /// <summary>
    /// All throw logic is factored out of the public extension methods and put in this helper class. This 
    /// allows more methods to be a candidate for inlining by the JIT compiler.
    /// </summary>
    internal static class Throw
    {
        internal static void ValueShouldNotBeNull<T>(Validator<T> validator, string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldNotBeNull,
                conditionDescription, validator.ArgumentName);
                
            throw validator.BuildException(condition);
        }

        internal static void ValueShouldBeBetween<T>(Validator<T> validator, T minValue, T maxValue,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeBetweenXAndY,
                conditionDescription, validator.ArgumentName, minValue.Stringify(), maxValue.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType = 
                GetEnumViolationOrDefault<T>(ConstraintViolationType.OutOfRangeViolation);

            throw validator.BuildException(condition, additionalMessage, violationType);
        }

        internal static void ValueShouldBeEqualTo<T>(Validator<T> validator, T value, 
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeEqualToX,
                conditionDescription, validator.ArgumentName, value.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType = GetEnumViolationOrDefault<T>();

            throw validator.BuildException(condition, additionalMessage, violationType);
        }

        internal static void ValueShouldBeNull<T>(Validator<T> validator, string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeNull,
                conditionDescription, validator.ArgumentName);

            throw validator.BuildException(condition);
        }

        internal static void ValueShouldBeGreaterThan<T>(Validator<T> validator, T minValue,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeGreaterThanX,
                conditionDescription, validator.ArgumentName, minValue.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType =
                GetEnumViolationOrDefault<T>(ConstraintViolationType.OutOfRangeViolation);

            throw validator.BuildException(condition, additionalMessage, violationType);
        }

        internal static void ValueShouldNotBeGreaterThan<T>(Validator<T> validator, T minValue,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldNotBeGreaterThanX,
                conditionDescription, validator.ArgumentName, minValue.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType =
                GetEnumViolationOrDefault<T>(ConstraintViolationType.OutOfRangeViolation);

            throw validator.BuildException(condition, additionalMessage, violationType);
        }

        internal static void ValueShouldBeGreaterThanOrEqualTo<T>(Validator<T> validator, T minValue,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeGreaterThanOrEqualToX,
               conditionDescription, validator.ArgumentName, minValue.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType type =
                GetEnumViolationOrDefault<T>(ConstraintViolationType.OutOfRangeViolation);

            throw validator.BuildException(condition, additionalMessage, type);
        }

        internal static void ValueShouldNotBeGreaterThanOrEqualTo<T>(Validator<T> validator, T maxValue,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldNotBeGreaterThanOrEqualToX,
               conditionDescription, validator.ArgumentName, maxValue.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType type =
                GetEnumViolationOrDefault<T>(ConstraintViolationType.OutOfRangeViolation);

            throw validator.BuildException(condition, additionalMessage, type);
        }

        internal static void ValueShouldBeSmallerThan<T>(Validator<T> validator, T maxValue,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeSmallerThanX,
               conditionDescription, validator.ArgumentName, maxValue.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType =
                GetEnumViolationOrDefault<T>(ConstraintViolationType.OutOfRangeViolation);

            throw validator.BuildException(condition, additionalMessage, violationType);
        }

        internal static void ValueShouldNotBeSmallerThan<T>(Validator<T> validator, T minValue,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldNotBeSmallerThanX,
               conditionDescription, validator.ArgumentName, minValue.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType =
                GetEnumViolationOrDefault<T>(ConstraintViolationType.OutOfRangeViolation);

            throw validator.BuildException(condition, additionalMessage, violationType);
        }

        internal static void ValueShouldBeSmallerThanOrEqualTo<T>(Validator<T> validator, T maxValue,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeSmallerThanOrEqualToX,
               conditionDescription, validator.ArgumentName, maxValue.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType =
                GetEnumViolationOrDefault<T>(ConstraintViolationType.OutOfRangeViolation);

            throw validator.BuildException(condition, additionalMessage, violationType);
        }

        internal static void ValueShouldNotBeSmallerThanOrEqualTo<T>(Validator<T> validator, T minValue,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldNotBeSmallerThanOrEqualToX,
               conditionDescription, validator.ArgumentName, minValue.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType =
                GetEnumViolationOrDefault<T>(ConstraintViolationType.OutOfRangeViolation);

            throw validator.BuildException(condition, additionalMessage, violationType);
        }

        internal static void ExpressionEvaluatedFalse<T>(Validator<T> validator, string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeValid,
                conditionDescription, validator.ArgumentName);

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType = GetEnumViolationOrDefault<T>();

            throw validator.BuildException(condition, additionalMessage, violationType);
        }

        internal static void LambdaXShouldHoldForValue<T>(Validator<T> validator, LambdaExpression lambda,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.LambdaXShouldHoldForValue,
                conditionDescription, validator.ArgumentName, lambda.Body.ToString());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType = GetEnumViolationOrDefault<T>();

            throw validator.BuildException(condition, additionalMessage, violationType);
        }
        
        internal static void ValueShouldBeNullOrAnEmptyString(Validator<string> validator,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.StringShouldBeNullOrEmpty,
                conditionDescription, validator.ArgumentName);

            throw validator.BuildException(condition);
        }

        internal static void ValueShouldBeAnEmptyString(Validator<string> validator, 
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.StringShouldBeEmpty,
                conditionDescription, validator.ArgumentName);

            throw validator.BuildException(condition);
        }

        internal static void ValueShouldNotBeAnEmptyString(Validator<string> validator,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.StringShouldNotBeEmpty,
                conditionDescription, validator.ArgumentName);

            throw validator.BuildException(condition);
        }

        internal static void ValueShouldNotBeNullOrAnEmptyString(Validator<string> validator,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.StringShouldNotBeNullOrEmpty,
                conditionDescription, validator.ArgumentName);

            throw validator.BuildException(condition);
        }

        internal static void ValueShouldBeUnequalTo<T>(Validator<T> validator, T value,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeUnequalToX,
                conditionDescription, validator.ArgumentName, value.Stringify());

            ConstraintViolationType violationType = GetEnumViolationOrDefault<T>();

            throw validator.BuildException(condition, violationType);
        }

        internal static void ValueShouldNotBeBetween<T>(Validator<T> validator, T minValue, T maxValue,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldNotBeBetweenXAndY,
                conditionDescription, validator.ArgumentName, minValue.Stringify(), maxValue.Stringify());

            string additionalMessage = GetActualValueMessage(validator);
            ConstraintViolationType violationType = GetEnumViolationOrDefault<T>();

            throw validator.BuildException(condition, additionalMessage, violationType);
        }

        internal static void StringShouldHaveLength(Validator<string> validator, int length,
            string conditionDescription)
        {
            string condition;

            if (length == 1)
            {
                condition = GetFormattedConditionMessage(validator, SR.StringShouldBe1CharacterLong,
                    conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition = GetFormattedConditionMessage(validator, SR.StringShouldBeXCharactersLong,
                    conditionDescription, validator.ArgumentName, length);
            }

            string additionalMessage = GetActualStringLengthMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void StringShouldNotHaveLength(Validator<string> validator, int length,
            string conditionDescription)
        {
            string condition;

            if (length == 1)
            {
                condition = GetFormattedConditionMessage(validator, SR.StringShouldNotBe1CharacterLong,
                    conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition = GetFormattedConditionMessage(validator, SR.StringShouldNotBeXCharactersLong,
                    conditionDescription, validator.ArgumentName, length);
            }

            throw validator.BuildException(condition);
        }

        internal static void StringShouldBeLongerThan(Validator<string> validator, int minLength,
            string conditionDescription)
        {
            string condition;

            if (minLength == 1)
            {
                condition = GetFormattedConditionMessage(validator, SR.StringShouldBeLongerThan1Character,
                    conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition = GetFormattedConditionMessage(validator, SR.StringShouldBeLongerThanXCharacters,
                    conditionDescription, validator.ArgumentName, minLength);
            }

            string additionalMessage = GetActualStringLengthMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void StringShouldBeShorterThan(Validator<string> validator, int maxLength,
            string conditionDescription)
        {
            string condition;

            if (maxLength == 1)
            {
                condition = GetFormattedConditionMessage(validator, SR.StringShouldBeShorterThan1Character,
                    conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition = GetFormattedConditionMessage(validator, SR.StringShouldBeShorterThanXCharacters,
                    conditionDescription, validator.ArgumentName, maxLength);
            }

            string additionalMessage = GetActualStringLengthMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void StringShouldBeShorterOrEqualTo(Validator<string> validator, int maxLength,
            string conditionDescription)
        {
            string condition;

            if (maxLength == 1)
            {
                condition = 
                    GetFormattedConditionMessage(validator, SR.StringShouldBeShorterOrEqualTo1Character,
                    conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition = 
                    GetFormattedConditionMessage(validator, SR.StringShouldBeShorterOrEqualToXCharacters,
                    conditionDescription, validator.ArgumentName, maxLength);
            }

            string additionalMessage = GetActualStringLengthMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void StringShouldBeLongerOrEqualTo(Validator<string> validator, int minLength,
            string conditionDescription)
        {
            string condition;

            if (minLength == 1)
            {
                condition =
                    GetFormattedConditionMessage(validator, SR.StringShouldBeLongerOrEqualTo1Character,
                    conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition =
                    GetFormattedConditionMessage(validator, SR.StringShouldBeLongerOrEqualToXCharacters,
                    conditionDescription, validator.ArgumentName, minLength);
            }

            string additionalMessage = GetActualStringLengthMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void StringShouldContain(Validator<string> validator, string value,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.StringShouldContainX,
                conditionDescription, validator.ArgumentName, value.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void StringShouldNotContain(Validator<string> validator, string value,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.StringShouldNotContainX,
                conditionDescription, validator.ArgumentName, value.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void StringShouldNotEndWith(Validator<string> validator, string value,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.StringShouldNotEndWithX,
                conditionDescription, validator.ArgumentName, value.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void StringShouldNotStartWith(Validator<string> validator, string value,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.StringShouldNotStartWithX,
                conditionDescription, validator.ArgumentName, value.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void StringShouldEndWith(Validator<string> validator, string value,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.StringShouldEndWithX,
                conditionDescription, validator.ArgumentName, value.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void StringShouldStartWith(Validator<string> validator, string value,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.StringShouldStartWithX,
                conditionDescription, validator.ArgumentName, value.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void ValueShouldBeOfType<T>(Validator<T> validator, Type type,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeOfTypeX,
                conditionDescription, validator.ArgumentName, type.Name);

            throw validator.BuildException(condition);
        }

        internal static void ValueShouldNotBeOfType<T>(Validator<T> validator, Type type,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldNotBeOfTypeX,
                conditionDescription, validator.ArgumentName, type.Name);

            throw validator.BuildException(condition);
        }

        internal static void ValueShouldBeTrue<T>(Validator<T> validator, string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeTrue,
                conditionDescription, validator.ArgumentName);

            throw validator.BuildException(condition);
        }

        internal static void ValueShouldBeFalse<T>(Validator<T> validator, string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.ValueShouldBeFalse,
                conditionDescription, validator.ArgumentName);
            
            throw validator.BuildException(condition);
        }

        internal static void CollectionShouldBeEmpty<T>(Validator<T> validator, string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.CollectionShouldBeEmpty,
                conditionDescription, validator.ArgumentName);

            throw validator.BuildException(condition);
        }

        internal static void CollectionShouldNotBeEmpty<T>(Validator<T> validator,
            string conditionDescription) where T : IEnumerable
        {
            string condition = GetFormattedConditionMessage(validator, SR.CollectionShouldNotBeEmpty,
                conditionDescription, validator.ArgumentName);

            throw validator.BuildException(condition);
        }

        internal static void CollectionShouldContain<T>(Validator<T> validator, object value,
            string conditionDescription) where T : IEnumerable
        {
            string condition = GetFormattedConditionMessage(validator, SR.CollectionShouldContainX,
                conditionDescription, validator.ArgumentName, value.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void CollectionShouldNotContain<T>(Validator<T> validator, object value,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.CollectionShouldNotContainX,
                conditionDescription, validator.ArgumentName, value.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void CollectionShouldContainAtLeastOneOf<T>(Validator<T> validator,
            IEnumerable values, string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.CollectionShouldContainAtLeastOneOfX,
                conditionDescription, validator.ArgumentName, values.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void CollectionShouldNotContainAnyOf<T>(Validator<T> validator, IEnumerable values,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.CollectionShouldNotContainAnyOfX,
               conditionDescription, validator.ArgumentName, values.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void CollectionShouldContainAllOf<T>(Validator<T> validator, IEnumerable values,
            string conditionDescription) where T : IEnumerable
        {
            string condition = GetFormattedConditionMessage(validator, SR.CollectionShouldContainAllOfX,
               conditionDescription, validator.ArgumentName, values.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void CollectionShouldNotContainAllOf<T>(Validator<T> validator, IEnumerable values,
            string conditionDescription)
        {
            string condition = GetFormattedConditionMessage(validator, SR.CollectionShouldNotContainAllOfX,
               conditionDescription, validator.ArgumentName, values.Stringify());

            throw validator.BuildException(condition);
        }

        internal static void CollectionShouldContainNumberOfElements<T>(Validator<T> validator, 
            int numberOfElements, string conditionDescription) where T : IEnumerable
        {
            string condition;

            if (numberOfElements == 1)
            {
                condition = GetFormattedConditionMessage(validator, SR.CollectionShouldContain1Element,
                    conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition = GetFormattedConditionMessage(validator, SR.CollectionShouldContainXElements,
                    conditionDescription, validator.ArgumentName, numberOfElements);
            }
            
            throw validator.BuildException(condition, GetCollectionContainsElementsMessage(validator));
        }

        internal static void CollectionShouldNotContainNumberOfElements<T>(Validator<T> validator, 
            int numberOfElements, string conditionDescription)
        {
            string condition;

            if (numberOfElements == 1)
            {
                condition = GetFormattedConditionMessage(validator, SR.CollectionShouldNotContain1Element,
                   conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition = GetFormattedConditionMessage(validator, SR.CollectionShouldNotContainXElements,
                   conditionDescription, validator.ArgumentName, numberOfElements);
            }

            throw validator.BuildException(condition);
        }

        internal static void CollectionShouldContainLessThan<T>(Validator<T> validator, int numberOfElements,
            string conditionDescription) where T : IEnumerable
        {
            string condition;

            if (numberOfElements == 1)
            {
                condition = GetFormattedConditionMessage(validator, SR.CollectionShouldContainLessThan1Element,
                    conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition = GetFormattedConditionMessage(validator, SR.CollectionShouldContainLessThanXElements,
                    conditionDescription, validator.ArgumentName, numberOfElements);
            }

            string additionalMessage = GetCollectionContainsElementsMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void CollectionShouldNotContainLessThan<T>(Validator<T> validator,
            int numberOfElements, string conditionDescription) where T : IEnumerable
        {
            string condition;

            if (numberOfElements == 1)
            {
                condition = 
                    GetFormattedConditionMessage(validator, SR.CollectionShouldNotContainLessThan1Element,
                        conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition = 
                    GetFormattedConditionMessage(validator, SR.CollectionShouldNotContainLessThanXElements,
                        conditionDescription, validator.ArgumentName, numberOfElements);
            }

            string additionalMessage = GetCollectionContainsElementsMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void CollectionShouldContainLessOrEqual<T>(Validator<T> validator,
            int numberOfElements, string conditionDescription) where T : IEnumerable
        {
            string condition =
                GetFormattedConditionMessage(validator, SR.CollectionShouldContainXOrLessElements, 
                conditionDescription, validator.ArgumentName, numberOfElements);

            string additionalMessage = GetCollectionContainsElementsMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void CollectionShouldNotContainLessOrEqual<T>(Validator<T> validator,
            int numberOfElements, string conditionDescription) where T : IEnumerable
        {
            string condition =
               GetFormattedConditionMessage(validator, SR.CollectionShouldNotContainXOrLessElements,
               conditionDescription, validator.ArgumentName, numberOfElements);

            string additionalMessage = GetCollectionContainsElementsMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void CollectionShouldContainMoreThan<T>(Validator<T> validator, int numberOfElements,
            string conditionDescription) where T : IEnumerable
        {
            string condition;

            if (numberOfElements == 1)
            {
                condition = GetFormattedConditionMessage(validator, SR.CollectionShouldContainMoreThan1Element,
                    conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition = GetFormattedConditionMessage(validator, SR.CollectionShouldContainMoreThanXElements,
                    conditionDescription, validator.ArgumentName, numberOfElements);
            }

            string additionalMessage = GetCollectionContainsElementsMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void CollectionShouldNotContainMoreThan<T>(Validator<T> validator,
            int numberOfElements, string conditionDescription) where T : IEnumerable
        {
            string condition;

            if (numberOfElements == 1)
            {
                condition = 
                    GetFormattedConditionMessage(validator, SR.CollectionShouldNotContainMoreThan1Element,
                    conditionDescription, validator.ArgumentName);
            }
            else
            {
                condition =
                    GetFormattedConditionMessage(validator, SR.CollectionShouldNotContainMoreThanXElements,
                    conditionDescription, validator.ArgumentName, numberOfElements);
            }

            string additionalMessage = GetCollectionContainsElementsMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void CollectionShouldContainMoreOrEqual<T>(Validator<T> validator,
            int numberOfElements, string conditionDescription) where T : IEnumerable
        {
            string condition = 
                GetFormattedConditionMessage(validator, SR.CollectionShouldContainXOrMoreElements,
                    conditionDescription, validator.ArgumentName, numberOfElements);
            
            string additionalMessage = GetCollectionContainsElementsMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        internal static void CollectionShouldNotContainMoreOrEqual<T>(Validator<T> validator,
            int numberOfElements, string conditionDescription) where T : IEnumerable
        {
            string condition =
                GetFormattedConditionMessage(validator, SR.CollectionShouldNotContainXOrMoreElements,
                    conditionDescription, validator.ArgumentName, numberOfElements);

            string additionalMessage = GetCollectionContainsElementsMessage(validator);

            throw validator.BuildException(condition, additionalMessage);
        }

        // This method returns extra information about the value of the validator.
        private static string GetActualValueMessage<T>(Validator<T> validator)
        {
            object value = validator.Value;

            // When the ToString method of the given type isn't overloaded, it returns the Type.FullName.
            // This information isn't very useful to the user, so in that case, we'll simply return null,
            // meaning: no extra information.
            if (value == null || value.GetType().FullName != value.ToString())
            {
                return SR.GetString(SR.TheActualValueIsX, validator.ArgumentName, validator.Value.Stringify());
            }

            return null;
        }

        private static string GetActualStringLengthMessage(Validator<string> validator)
        {
            int length = validator.Value != null ? validator.Value.Length : 0;

            if (length == 1)
            {
                return SR.GetString(SR.TheActualValueIs1CharacterLong, validator.ArgumentName);
            }
            else
            {
                return SR.GetString(SR.TheActualValueIsXCharactersLong, validator.ArgumentName, length);
            }
        }

        private static string GetCollectionContainsElementsMessage<T>(Validator<T> validator)
            where T : IEnumerable
        {
            if (validator.Value == null)
            {
                return SR.GetString(SR.CollectionIsCurrentlyANullReference, validator.ArgumentName);
            }
            else
            {
                int numberOfElements = GetNumberOfElements(validator.Value);

                if (numberOfElements == 1)
                {
                    return SR.GetString(SR.CollectionContainsCurrently1Element, validator.ArgumentName);
                }
                else
                {
                    return SR.GetString(SR.CollectionContainsCurrentlyXElements, validator.ArgumentName,
                       numberOfElements);
                }
            }
        }

        // Returns the number of elements in the sequence.
        private static int GetNumberOfElements(IEnumerable sequence)
        {
            ICollection collection = sequence as ICollection;

            if (collection != null)
            {
                return collection.Count;
            }

            IEnumerator enumerator = sequence.GetEnumerator();
            try
            {
                int count = 0;
                while (enumerator.MoveNext())
                {
                    count++;
                }

                return count;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        // Returns the 'InvalidEnumViolation' when the T is an Enum and otherwise 'Default'.
        private static ConstraintViolationType GetEnumViolationOrDefault<T>()
        {
            return GetEnumViolationOrDefault<T>(ConstraintViolationType.Default);
        }

        // Returns the 'InvalidEnumViolation' when the T is an Enum and otherwise the specified defaultValue.
        private static ConstraintViolationType GetEnumViolationOrDefault<T>(
            ConstraintViolationType defaultValue)
        {
            if (typeof(T).IsEnum)
            {
                return ConstraintViolationType.InvalidEnumViolation;
            }
            else
            {
                return defaultValue;
            }
        }

        private static string GetFormattedConditionMessage<T>(Validator<T> validator, string resourceKey,
            string conditionDescription, params object[] resourceFormatArguments)
        {
            if (conditionDescription != null)
            {
                return FormatConditionDescription(validator, conditionDescription);
            }
            else
            {
                return SR.GetString(resourceKey, resourceFormatArguments);
            }
        }

        private static string FormatConditionDescription<T>(Validator<T> validator, 
            string conditionDescription)
        {
            try
            {
                return String.Format(CultureInfo.CurrentCulture, conditionDescription ?? String.Empty,
                    validator.ArgumentName);
            }
            catch (FormatException)
            {
                // We catch a FormatException. This code should only throw exceptions generated by the
                // validator.BuildException method. Throwing another exception would confuse the user and
                // would make debugging harder. When the user supplied an unformattable description, we simply
                // use the unformatted description as condition.
                return conditionDescription;
            }
        }
    }
}