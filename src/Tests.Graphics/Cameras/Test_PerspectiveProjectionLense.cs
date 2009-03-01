using Afterglow.Math;
using MbUnit.Framework;

namespace Afterglow.Graphics.Cameras
{
    public class Test_PerspectiveProjectionLense
    {
        [Test]
        public void Constructor_initializes_AspectRatio_HorizontalFoV_DistanceToFarPlane_DistanceToNearPlane()
        {
            var lense = new PerspectiveProjectionLense();

            Assert.IsNotNull(lense);
            Assert.AreEqual(4.0f / 3.0f, lense.AspectRatio);
            Assert.AreEqual(0.75f, lense.HorizontalFieldOfView);
            Assert.AreEqual(0.001f, lense.DistanceToNearPlane);
            Assert.AreEqual(100000f, lense.DistanceToFarPlane);
        }

        [Test]
        public void ProjectionMatrix()
        {
            var lense = new PerspectiveProjectionLense
            {
                AspectRatio = (16.0f / 9.0f),
                HorizontalFieldOfView = 1,
                DistanceToNearPlane = 0.1f,
                DistanceToFarPlane = 1000
            };

            var expected = new Matrix(
                1.946985f, 0, 0, 0,
                0, 3.461308f, 0, 0,
                0, 0, -1.0002f, -1,
                0, 0, -0.10001f, 0);

            Assert.AreEqual(expected, lense.ProjectionMatrix);
        }
    }
}