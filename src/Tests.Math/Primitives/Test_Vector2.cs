using MbUnit.Framework;

namespace TheNewEngine.Math
{
    public class Test_Vector2
    {
        [Test]
        public void Constructor_Sets_XY()
        {
            const float X = 1.0f;
            const float Y = 2.0f;

            var vector2 = new Vector2(X, Y);

            Assert.AreEqual(X, vector2.X);
            Assert.AreEqual(Y, vector2.Y);
        }

        [Test]
        public void Constructor_with_array_as_argument()
        {
            const float X = 1.0f;
            const float Y = 2.0f;
            var values = new[] { X, Y };

            var vector2 = new Vector2(values);

            Assert.AreEqual(X, vector2.X);
            Assert.AreEqual(Y, vector2.Y);
        }

        [Test]
        public void Zero()
        {
            var zero = Vector2.Zero;

            Assert.AreEqual(new Vector2(0, 0), zero);
        }

        [Test]
        public void XAxis()
        {
            var xAxis = Vector2.XAxis;

            Assert.AreEqual(new Vector2(1, 0), xAxis);
        }

        [Test]
        public void YAxis()
        {
            var yAxis = Vector2.YAxis;

            Assert.AreEqual(new Vector2(0, 1), yAxis);
        }

        [Test]
        public new void ToString()
        {
            var vector = new Vector2(1.1f, 2.2f);

            Assert.AreEqual("X: 1.1 Y: 2.2", vector.ToString());
        }
    }
}