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
    }
}