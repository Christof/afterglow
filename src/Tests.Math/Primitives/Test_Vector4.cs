using MbUnit.Framework;

namespace TheNewEngine.Math.Primitives
{
    public class Test_Vector4
    {
        [Test]
        public void Constructor_Sets_XYZ()
        {
            const float X = 1.0f;
            const float Y = 2.0f;
            const float Z = 3.0f;
            const float W = 4.0f;

            var vector4 = new Vector4(X, Y, Z, W);

            Assert.AreEqual(X, vector4.X);
            Assert.AreEqual(Y, vector4.Y);
            Assert.AreEqual(Z, vector4.Z);
            Assert.AreEqual(W, vector4.W);
        }

        [Test]
        public void AccessValuesByIndex()
        {
            const float X = 1.0f;
            const float Y = 2.0f;
            const float Z = 3.0f;
            const float W = 4.0f;

            var vector4 = new Vector4(X, Y, Z, W);

            Assert.AreEqual(X, vector4[0]);
            Assert.AreEqual(Y, vector4[1]);
            Assert.AreEqual(Z, vector4[2]);
            Assert.AreEqual(W, vector4[3]);
        }
    }
}