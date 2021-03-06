using Afterglow.Math;
using MbUnit.Framework;

namespace Afterglow.Graphics.Cameras
{
    public class Test_Stand
    {
        [Test]
        public void Constructor_initializes_Up_Direction_and_Position()
        {
            var stand = new Stand();

            Assert.IsNotNull(stand);
            Assert.AreEqual(Vector3.YAxis, stand.Up);
            Assert.AreEqual(-Vector3.ZAxis, stand.Direction);
            Assert.AreEqual(Vector3.Zero, stand.Position);
        }

        [Test]
        public void Up_set_and_get()
        {
            var stand = new Stand();
            var up = new Vector3(1, 2, 3);

            stand.Up = up;

            Assert.AreEqual(up, stand.Up);
        }

        [Test]
        public void Direction_set_and_get()
        {
            var stand = new Stand();
            var direction = new Vector3(4, 5, 6);

            stand.Direction = direction;

            Assert.AreEqual(direction, stand.Direction);
        }

        [Test]
        public void Position_set_and_get()
        {
            var stand = new Stand();
            var position = new Vector3(7, 8, 9);

            stand.Position = position;

            Assert.AreEqual(position, stand.Position);
        }

        [Test]
        public void ViewMatrix()
        {
            var stand = new Stand
            {
                Up = new Vector3(1, 2, 3),
                Direction = new Vector3(4, 5, 6),
                Position = new Vector3(7, 8, 9)
            };

            var expected = new Matrix(
                 0.4082483f,  -0.7909116f, -0.4558423f, 0,
                -0.8164966f, -0.09304842f, -0.5698029f, 0,
                 0.4082483f,   0.6048148f, -0.6837634f, 0,
                         0f,   0.8374357f,   13.90319f, 1);

            Assert.AreEqual(expected, stand.ViewMatrix);
        }
    }
}