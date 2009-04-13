using Afterglow.Graphics.GraphicStreams;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using OpenTK;

namespace Afterglow.Graphics.OpenTK.GraphicStreams
{
    public class Test_OpenTKBuffer
    {
        [Test]
        public void Handle_is_invalid_after_construction()
        {
            new OpenTKBuffer().Handle.ShouldEqual(OpenTKBuffer.INVALID_HANDLE); 
        }

        [Test]
        public void Load_OnFrame_Unload_Cycle()
        {
            // call to initialize OpenGL
            new GLControl();
            
            var data = new[] { 1f, 2f, 3f };
            var stream = new GraphicStream<float>(GraphicStreamUsage.Position, data);

            var buffer = new OpenTKBuffer();

            buffer.Load(stream);
            buffer.Handle.ShouldNotEqual(OpenTKBuffer.INVALID_HANDLE);

            buffer.Unload();
            buffer.Handle.ShouldEqual(OpenTKBuffer.INVALID_HANDLE); 
        }
    }
}