using Autofac;

namespace Afterglow.Infrastructure
{
    /// <summary>
    /// Resolves dependencies with an underlying IoC-Framework.
    /// </summary>
    public static class DependencyResolver
    {
        private static IContainer mContainer;

        /// <summary>
        /// Sets the container.
        /// </summary>
        /// <param name="container">The container.</param>
        public static void SetContainer(IContainer container)
        {
            mContainer = container;
        }

        /// <summary>
        /// Resolves the dependencies for the given generic type and
        /// returns the resolved implementation.
        /// </summary>
        /// <typeparam name="T">Type which should be resolved.</typeparam>
        /// <returns>Resolved instantiated implementation.</returns>
        public static T Resolve<T>()
        {
            return mContainer.Resolve<T>();
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
            return mContainer.Resolve<T>(new NamedParameter(parameterName, value));
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
            return mContainer.Resolve<T>(new TypedParameter(typeof (ArgumentType), value));
        }
    }
}