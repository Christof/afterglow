using System.Collections.Generic;

namespace TheNewEngine.Infrastructure
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
        /// Divides the given enumerable into slices of the given size.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the enumerable</typeparam>
        /// <param name="enumerable">The sequence.</param>
        /// <param name="size">The size of slice.</param>
        /// <returns>A enumerable of slices with the given size.</returns>
        public static IEnumerable<T[]> Slice<T>(this IEnumerable<T> enumerable, int size)
        {
            int counter = 0;
            var slice = new T[size];

            foreach (var item in enumerable)
            {
                slice[counter++] = item;

                if (counter == size)
                {
                    yield return slice;
                    counter = 0;
                    slice = new T[size];
                }
            }
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
    }
}