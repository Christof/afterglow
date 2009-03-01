using Afterglow.Math;
using MbUnit.Framework;
using SlimDXMatrix = SlimDX.Matrix;

namespace Afterglow.Graphics.Cameras
{
    public class Test_Comparison_Stand
    {
        [Test]
        public void CalcualteViewMatrix()
        {
            var position = new Vector3(2, 3, 5);
            var direction = new Vector3(0, 0, 1);
            var up = new Vector3(0, 1, 0);

            var view = Stand.CalculateViewMatrix(position, direction, up);

            var slimDX = SlimDXMatrix.LookAtRH(
                position.ToSlimDX(), (position + direction).ToSlimDX(), up.ToSlimDX());

            Assert.AreEqual(slimDX.ToMath(), view);
        }

        [Test]
        public void CalcualteViewMatrix_with_simple_values()
        {
            var position = new Vector3(0, 0, 3);
            var direction = new Vector3(0, 0, -1);
            var up = new Vector3(0, 1, 0);

            var view = Stand.CalculateViewMatrix(position, direction, up);

            var slimDX = SlimDXMatrix.LookAtRH(
                position.ToSlimDX(), (position + direction).ToSlimDX(), up.ToSlimDX());

            Assert.AreEqual(slimDX.ToMath(), view);
        }
    }
}