using System.Collections.Generic;

namespace TheNewEngine.Graphics.Resources
{
    /// <summary>
    /// Resource which contains other <see cref="IResource"/> of the
    /// generic type.
    /// </summary>
    /// <typeparam name="ItemType">The type of an item in the container.</typeparam>
    public interface IResourceContainer<ItemType> : IResource, IEnumerable<ItemType>
        where ItemType : IResource
    {
        /// <summary>
        /// Adds the frame resource to the container.
        /// </summary>
        /// <param name="frameResource">The frame resource.</param>
        void Add(ItemType frameResource);
    }
}