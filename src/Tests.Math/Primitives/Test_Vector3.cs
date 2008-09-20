using MbUnit.Framework;

namespace TheNewEngine.Math.Primitives
{
    [TestFixture]
    public class Test_Vector3
    {
        [Test]
        public void Constructor_Sets_XYZ()
        {
            const float X = 1.0f;
            const float Y = 2.0f;
            const float Z = 3.0f;

            var vector3 = new Vector3(X, Y, Z);

            Assert.AreEqual(X, vector3.X);
            Assert.AreEqual(Y, vector3.Y);
            Assert.AreEqual(Z, vector3.Z);
        }

        [Test]
        public void AccessValuesByIndex()
        {
            const float X = 1.0f;
            const float Y = 2.0f;
            const float Z = 3.0f;

            var vector3 = new Vector3(X, Y, Z);

            Assert.AreEqual(X, vector3[0]);
            Assert.AreEqual(Y, vector3[1]);
            Assert.AreEqual(Z, vector3[2]);
        }

        [Test]
        public void LengthSquared()
        {
            var vector3 = new Vector3(2, 3, 4);

            Assert.AreEqual(2 * 2 + 3 * 3 + 4 * 4, vector3.LengthSquared);
        }

        [Test]
        public void Length()
        {
            var vector3 = new Vector3(2, 3, 4);

            Assert.AreEqual((float)System.Math.Sqrt(2 * 2 + 3 * 3 + 4 * 4), vector3.Length);
        }

        [Test]
        public void Normalize()
        {
            var original = new Vector3(1, 3, 5);
            var vector3 = original;

            vector3.Normalize();

            var expected = new Vector3(original.X / original.Length, 
                original.Y / original.Length, original.Z / original.Length);

            Assert.AreEqual(expected, vector3);
        }

        [Test]
        public void Cross()
        {
            var vector1 = new Vector3(2.0f, 3.0f, 4.0f);
            var vector2 = new Vector3(5.0f, 6.0f, 7.0f);

            var cross = vector1.Cross(vector2);
            Assert.AreEqual(-3.0f, cross.X);
            Assert.AreEqual(6.0f, cross.Y);
            Assert.AreEqual(-3.0f, cross.Z);
        }
    }
}