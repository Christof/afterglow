using MbUnit.Framework;
using Moq;
using Afterglow.Math;

namespace Afterglow.Graphics.GraphicStreams
{
    public class Test_BufferServiceExtensions
    {
        [Test]
        public void CreateFor_with_uint()
        {
            var concreteStream = new GraphicStream<uint>(
                GraphicStreamUsage.Index, new uint[0]);
            IGraphicStream stream = concreteStream;

            var bufferServiceMock = new Mock<IBufferService>();
            
            bufferServiceMock.Object.CreateFor(stream);

            bufferServiceMock.Verify(s => s.CreateFor(concreteStream));
        }

        [Test]
        public void CreateFor_with_Vector2()
        {
            var concreteStream = new GraphicStream<Vector2>(
                GraphicStreamUsage.Index, new Vector2[0]);
            IGraphicStream stream = concreteStream;

            var bufferServiceMock = new Mock<IBufferService>();

            bufferServiceMock.Object.CreateFor(stream);

            bufferServiceMock.Verify(s => s.CreateFor(concreteStream));
        }

        [Test]
        public void CreateFor_with_Vector3()
        {
            var concreteStream = new GraphicStream<Vector3>(
                GraphicStreamUsage.Index, new Vector3[0]);
            IGraphicStream stream = concreteStream;

            var bufferServiceMock = new Mock<IBufferService>();

            bufferServiceMock.Object.CreateFor(stream);

            bufferServiceMock.Verify(s => s.CreateFor(concreteStream));
        }

        [Test]
        public void CreateFor_with_Vector4()
        {
            var concreteStream = new GraphicStream<Vector4>(
                GraphicStreamUsage.Index, new Vector4[0]);
            IGraphicStream stream = concreteStream;

            var bufferServiceMock = new Mock<IBufferService>();

            bufferServiceMock.Object.CreateFor(stream);

            bufferServiceMock.Verify(s => s.CreateFor(concreteStream));
        }

        //[Test]
        //public void CreateFor_with_Color4()
        //{
        //    var concreteStream = new GraphicStream<Vector4>(
        //        GraphicStreamUsage.Index, new Vector4[0]);
        //    IGraphicStream stream = concreteStream;

        //    var bufferServiceMock = new Mock<IBufferService>();

        //    bufferServiceMock.Object.CreateFor(stream);

        //    bufferServiceMock.Verify(s => s.CreateFor(concreteStream));
        //}

        [Test]
        public void CreateFor_with_Float()
        {
            var concreteStream = new GraphicStream<float>(
                GraphicStreamUsage.Index, new float[0]);
            IGraphicStream stream = concreteStream;

            var bufferServiceMock = new Mock<IBufferService>();

            bufferServiceMock.Object.CreateFor(stream);

            bufferServiceMock.Verify(s => s.CreateFor(concreteStream));
        }

        [Test]
        public void CreateFor_with_int()
        {
            var concreteStream = new GraphicStream<int>(
                GraphicStreamUsage.Index, new int[0]);
            IGraphicStream stream = concreteStream;

            var bufferServiceMock = new Mock<IBufferService>();

            bufferServiceMock.Object.CreateFor(stream);

            bufferServiceMock.Verify(s => s.CreateFor(concreteStream));
        }
    }
}