using System.Collections.Generic;
using System;

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

            foreach (var item in enumerable)
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
            foreach (var value in enumerable)
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
            foreach (var element in enumerable)
            {
                action(element);
            }
        }
    }
}