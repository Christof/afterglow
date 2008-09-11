using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheNewEngine.Graphics.SlimDX
{
    public interface IBuffer : IFrameResource
    {
        int Index { get; set; }
    }
}
