using System.Collections;
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
        /// 	Returns <c>true</c> if the string is <c>null</c> or empty; otherwise <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Divides the given sequence into slices of the given size.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="size">The size of slice.</param>
        /// <returns>A sequence of slices with the given size.</returns>
        public static IEnumerable<T[]> Slice<T>(this IEnumerable<T> sequence, int size)
        {
            int counter = 0;
            var slice = new T[size];

            foreach (var item in sequence)
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
    }
}