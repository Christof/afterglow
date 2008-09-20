using MbUnit.Framework;
using TheNewEngine.Graphics.Xna;
using TheNewEngine.Math.Primitives;

namespace TheNewEngine.Graphics.Cameras
{
    public class Test_Comparison_Stand
    {
        [Test]
        public void CalcualteViewMatrix()
        {
            var position = new Vector3();
            var direction = new Vector3(0, 0, 1);
            var up = new Vector3(0, 1, 0);

            var view = Stand.CalculateViewMatrix(position, direction, up);

            var xna = Microsoft.Xna.Framework.Matrix.CreateLookAt(
                position.ToXna(), (position + direction).ToXna(), up.ToXna());

            Assert.AreEqual(xna, view.ToXna());
        }
    }

}