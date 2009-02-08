using MbUnit.Framework;
using System;

namespace TheNewEngine.Infrastructure
{
    /// <summary>
    /// Contains extension to write expectations in a more speaking, bdd-style.
    /// </summary>
    public static class BddExtensions
    {
        /// <summary>
        /// The value should not be null.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The value for further expectations.</returns>
        public static T ShouldNotBeNull<T>(this T value)
            where T : class
        {
            Assert.IsNotNull(value);

            return value;
        }

        /// <summary>
        /// The value should be null.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="value">The value.</param>
        public static void ShouldBeNull<T>(this T value)
            where T : class
        {
            Assert.IsNull(value);
        }

        /// <summary>
        /// The value should equal the given expected value.
        /// </summary>
        /// <typeparam name="T">Type of the values.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        public static void ShouldEqual<T>(this T value, T expectedValue)
        {
            Assert.AreEqual(expectedValue, value);
        }

        /// <summary>
        /// The value should equal the given expected value considering the given delta.
        /// </summary>
        /// <typeparam name="T">Type of the values.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        /// <param name="delta">The delta.</param>
        public static void ShouldEqual<T>(this T value, T expectedValue, T delta)
        {
            Assert.AreApproximatelyEqual(expectedValue, value, delta);
        }

        /// <summary>
        /// The value should not be equal to the given unexpected value.
        /// </summary>
        /// <typeparam name="T">Type of the values.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="unexpectedValue">The unexpected value.</param>
        /// <returns>The value for further expectations.</returns>
        public static T ShouldNotEqual<T>(this T value, T unexpectedValue)
        {
            Assert.AreNotEqual(unexpectedValue, value);

            return value;
        }

        /// <summary>
        /// Should be the same as the expected value.
        /// </summary>
        /// <typeparam name="T">Type of the values which have to be a reference type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        public static void ShouldBeTheSameAs<T>(this T value, T expectedValue)
            where T : class
        {
            Assert.AreSame(expectedValue, value);
        }

        /// <summary>
        /// Should not be the same instance as the given unexpected value.
        /// </summary>
        /// <typeparam name="T">Type of the values which have to be a reference type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="unexpectedValue">The unexpected value.</param>
        /// <returns>The value.</returns>
        public static T ShouldNotBeTheSameAs<T>(this T value, T unexpectedValue)
            where T : class
        {
            Assert.AreNotSame(unexpectedValue, value);

            return value;
        }

        /// <summary>
        /// The value should be true.
        /// </summary>
        /// <param name="value">The bool value.</param>
        public static void ShouldBeTrue(this bool value)
        {
            Assert.IsTrue(value);
        }

        /// <summary>
        /// The value should be false.
        /// </summary>
        /// <param name="value">The bool value.</param>
        public static void ShouldBeFalse(this bool value)
        {
            Assert.IsFalse(value);
        }

        /// <summary>
        /// Gets the exception possibly thrown in the action or <c>null</c> if no exception is thrown.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>The exception thrown in the action; or <c>null</c>.</returns>
        public static Exception GetPossibleException(this Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                return exception;
            }

            return null;
        }

        /// <summary>
        /// Shoulds throw an exception of the given type.
        /// </summary>
        /// <typeparam name="ExceptionType">The type of the exception.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns>The exception.</returns>
        public static ExceptionType ShouldThrow<ExceptionType>(this Action action)
            where ExceptionType : Exception
        {
            return Assert.Throws<ExceptionType>(() => action());
        }

        /// <summary>
        /// Should be of the given expected type.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <typeparam name="ExpectedType">The expected type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The value.</returns>
        public static T ShouldBe<T, ExpectedType>(this T value)
        {
            Assert.IsInstanceOfType(typeof(ExpectedType), value);

            return value;
        }

        /// <summary>
        /// Should be of the expected type.
        /// </summary>
        /// <typeparam name="ExpectedType">The expected type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The value cast to the expected type.</returns>
        public static ExpectedType ShouldBe<ExpectedType>(this object value)
        {
            Assert.IsInstanceOfType(typeof(ExpectedType), value);

            return (ExpectedType)value;
        }
    }
}