using System.Collections;
using System.Collections.Generic;

namespace TheNewEngine.Graphics
{
    public interface IFrameResourceContainer<Type> : IFrameResource, IEnumerable<Type>
        where Type : IFrameResource
    {
        void Add(Type frameResource);
    }
}