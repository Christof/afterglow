using MbUnit.Framework;
using Afterglow.Graphics.SlimDX;
using SlimDXMatrix = SlimDX.Matrix;

namespace Afterglow.Graphics.Cameras
{
    public class Test_Comparison_PerspectiveProjectionLense
    {
        [Test]
        public void CalculateProjectionMatrix()
        {
            var distanceToNearPlane = 0.001f;
            var distanceToFarPlane = 100000f;
            var horizontalFieldOfView = 0.75f;
            var aspectRatio = 4.0f / 3.0f;

            var projection = PerspectiveProjectionLense.CalculateProjectionMatrix(
                distanceToNearPlane, distanceToFarPlane, horizontalFieldOfView, aspectRatio);

            var projectionSlimDX = SlimDXMatrix.PerspectiveFovRH(
                horizontalFieldOfView / aspectRatio, aspectRatio, 
                distanceToNearPlane, distanceToFarPlane);

            Assert.AreEqual(projectionSlimDX.ToMath(), projection);
        }
    }
}