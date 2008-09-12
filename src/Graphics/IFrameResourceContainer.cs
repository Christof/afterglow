using System.Collections.Generic;

namespace TheNewEngine.Graphics
{
    /// <summary>
    /// <see cref="IFrameResource"/> which contains other <see cref="IFrameResource"/> of the
    /// generic type.
    /// </summary>
    /// <typeparam name="ItemType">The type of an item in the container.</typeparam>
    public interface IFrameResourceContainer<ItemType> : IFrameResource, IEnumerable<ItemType>
        where ItemType : IFrameResource
    {
        /// <summary>
        /// Adds the frame resource to the container.
        /// </summary>
        /// <param name="frameResource">The frame resource.</param>
        void Add(ItemType frameResource);
    }
}