using System;
using System.Collections;
using System.Collections.Generic;

namespace Afterglow.Infrastructure
{
    /// <summary>
    /// Some handy extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns <c>true</c> if the string is <c>null</c> or empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// Returns <c>true</c> if the string is <c>null</c> or empty; otherwise <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Divides the given enumerable into slices of the given sliceSize.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable</typeparam>
        /// <param name="enumerable">The sequence.</param>
        /// <param name="sliceSize">The size of slice.</param>
        /// <returns>
        /// A enumerable of slices with the given sliceSize.
        /// </returns>
        public static IEnumerable<T[]> Slice<T>(this IEnumerable<T> enumerable, int sliceSize)
        {
            int counter = 0;
            var slice = new T[sliceSize];

            foreach (T item in enumerable)
            {
                slice[counter++] = item;

                if (counter == sliceSize)
                {
                    yield return slice;
                    counter = 0;
                    slice = new T[sliceSize];
                }
            }
        }

        /// <summary>
        /// Returns each nth plus offset element.
        /// </summary>
        /// <typeparam name="T">Type of an element in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="n">Number n to specify the nth elements.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>
        /// Each element with an index multiple of n plus offset.
        /// </returns>
        public static IEnumerable<T> EachNthElement<T>(
            this IEnumerable<T> enumerable, int n, int offset)
        {
            int index = 0;
            foreach (T value in enumerable)
            {
                if (index++ % n == offset)
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Returns each nth element.
        /// </summary>
        /// <typeparam name="T">Type of an element in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="n">Number n to specify the nth elements.</param>
        /// <returns>Each element with an index multiple of n.</returns>
        public static IEnumerable<T> EachNthElement<T>(
            this IEnumerable<T> enumerable, int n)
        {
            return enumerable.EachNthElement(n, 0);
        }

        /// <summary>
        /// Downcasts the specified base instance to the given target type.
        /// </summary>
        /// <typeparam name="TargetType">The target type.</typeparam>
        /// <param name="baseInstance">Instance of the base type.</param>
        /// <returns>Downcast representation of the base instance.</returns>
        public static TargetType DowncastTo<TargetType>(this object baseInstance)
        {
            return (TargetType)baseInstance;
        }

        /// <summary>
        /// Disposes the object if it's not null.
        /// </summary>
        /// <param name="disposable">The disposable.</param>
        public static void DisposeIfNotNull(this IDisposable disposable)
        {
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// Calls the given action for each element in the enumerable.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T element in enumerable)
            {
                action(element);
            }
        }

        /// <summary>
        /// Calls the given action for each element in the enumerable.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        public static void ForEach(this IEnumerable enumerable, Action<object> action)
        {
            foreach (object element in enumerable)
            {
                action(element);
            }
        }

        /// <summary>
        /// Transforms a given collection to any other, by doing that for each element
        /// </summary>
        /// <typeparam name="TSource">type of the source element</typeparam>
        /// <typeparam name="TDestination">type of the destination element</typeparam>
        /// <param name="source">source to transform</param>
        /// <param name="transformationOfOne">logic for transforming one element</param>
        /// <returns>array of the transformed elements</returns>
        public static IEnumerable<TDestination> TransformCollection<TSource, TDestination>(
            this IEnumerable<TSource> source,
            Func<TSource, TDestination> transformationOfOne)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (transformationOfOne == null)
                throw new ArgumentNullException("transformationOfOne");

            foreach (var element in source)
            {
                yield return transformationOfOne(element);
            }
        }

        /// <summary>
        /// Transforms a given collection to any other, by doing that for each element
        /// </summary>
        /// <typeparam name="TDestination">type of the destination element</typeparam>
        /// <param name="source">source to transform</param>
        /// <param name="transformationOfOne">logic for transforming one element</param>
        /// <returns>array of the transformed elements</returns>
        public static IEnumerable<TDestination> TransformObjectCollection<TDestination>(
            this IEnumerable source,
            Func<object, TDestination> transformationOfOne)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (transformationOfOne == null)
                throw new ArgumentNullException("transformationOfOne");

            foreach (var element in source)
            {
                yield return transformationOfOne(element);
            }
        }

        /// <summary>
        /// Tries to create an instance of the type. 
        /// </summary>
        /// <param name="type">Type of the instance to create</param>
        /// <returns>An instance of the type</returns>
        public static object TryCreateInstance(this Type type)
        {
            if (type.Equals(typeof(String)))
                return "Hello World";
            if (type.Equals(typeof(int)))
                return 42;
            if (type.Equals(typeof(object)))
                return 42;

            var defaultConstructor = type.GetConstructor(new Type[0]);
            if(defaultConstructor != null)
                return defaultConstructor.Invoke(null);

            var constructors = type.GetConstructors();
            foreach (var constructorInfo in constructors)
            {
                var parameters = constructorInfo.GetParameters();
                if(parameters.Length > 0)
                {
                    var parameterValues = new object[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var parameterType = parameters[i].ParameterType;
                        if(!parameterType.Equals(type))
                            parameterValues[i] = parameterType.TryCreateInstance();
                        else
                            break;
                    }

                    try
                    {
                        return constructorInfo.Invoke(parameterValues);
                    }
                    catch (Exception)
                    {
                        ;
                    }
                }
            }

            return null;
        }
    }
}