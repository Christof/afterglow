using MbUnit.Framework;
using TheNewEngine.Graphics.SlimDX;
using TheNewEngine.Math;
using SlimDXMatrix = SlimDX.Matrix;

namespace TheNewEngine.Graphics.Cameras
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
    }
}