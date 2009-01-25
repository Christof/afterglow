using MbUnit.Framework;
using Moq;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.Cameras
{
    public class Test_Camera
    {
        [Test]
        public void Constructor()
        {
            var stand = new Mock<IStand>().Object;
            var lense = new Mock<ILense>().Object;

            var cam = new Camera(stand, lense);

            Assert.IsNotNull(cam);
            Assert.AreSame(stand, cam.Stand);
            Assert.AreSame(lense, cam.Lense);
        }

        [Test]
        public void ViewProjectionMatrix()
        {
            var stand = new Mock<IStand>();
            var lense = new Mock<ILense>();

            var viewMatrix = new Matrix(11, 12, 13, 14, 21, 22, 23, 24, 31, 32, 33, 34, 41, 42, 43, 44);
            stand.SetupGet(x => x.ViewMatrix).Returns(viewMatrix);
            var projectionMatrix = new Matrix(0, 1, 2, 3, 10, 11, 12, 13, 20, 21, 22, 23, 30, 31, 32, 33);
            lense.SetupGet(x => x.ProjectionMatrix).Returns(projectionMatrix);

            var cam = new Camera(stand.Object, lense.Object);

            Assert.AreEqual(viewMatrix * projectionMatrix, cam.ViewProjectionMatrix);
        }
    }
}