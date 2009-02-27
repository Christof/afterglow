using System;
using System.ComponentModel;

namespace TheNewEngine.Infrastructure
{
    /// <summary>
    /// Hides the methods from object (ToString(), GetHashCode(), GetType()).
    /// </summary>
    public interface IFluentInterface
    {
        /// <summary>
        /// Hides the ToString method.
        /// </summary>
        /// <returns>The string.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        /// <summary>
        /// Hides the GetHashCode method.
        /// </summary>
        /// <returns>The hashcode.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary>
        /// Hides the GetType method.
        /// </summary>
        /// <returns>The type of the object.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();
        
        /// <summary>
        /// Hides the Equals method
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>Nothing in this case.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }
}