using StructureMap;

namespace TheNewEngine.Common
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
            return ObjectFactory.GetInstance<T>();
        }
    }
}