namespace TheNewEngine.Infrastructure
{
    /// <summary>
    /// Resolves dependencies with an underlying IoC-Framework.
    /// </summary>
    public static class DependencyResolver
    {
        /// <summary>
        /// Resolves the dependencies for the given generic type and
        /// returns the resolved implementation.
        /// </summary>
        /// <typeparam name="T">Type which should be resolved.</typeparam>
        /// <returns>Resolved instantiated implementation.</returns>
        public static T Resolve<T>()
        {
            return default(T);
        }

        /// <summary>
        /// Resolves the dependencies for the given generic type and uses the given
        /// parameter to construct the implementation.
        /// </summary>
        /// <typeparam name="T">Type which should be resolved.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns>Resolved instantiated implementation.</returns>
        public static T ResolveWith<T>(string parameterName, object value)
        {
            return default(T);
        }

        /// <summary>
        /// Resolves the dependencies for the given generic type and uses the given
        /// parameter to construct the implementation.
        /// </summary>
        /// <typeparam name="T">Type which should be resolved.</typeparam>
        /// <typeparam name="ArgumentType">The type of the argument.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>Resolved instantiated implementation.</returns>
        public static T ResolveWith<T, ArgumentType>(ArgumentType value)
        {
            return default(T);
        }
    }
}