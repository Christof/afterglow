using System;
using MbUnit.Framework;

namespace TheNewEngine.Math
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
        public void Constructor_with_array_as_argument()
        {
            const float X = 1.0f;
            const float Y = 2.0f;
            const float Z = 3.0f;
            const float W = 4.0f;
            var values = new[] { X, Y, Z, W };

            var vector4 = new Vector4(values);

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

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Access_values_by_index_throws_OutOfRangeException_if_index_is_out_of_range()
        {
            var vector4 = new Vector4();

            var value = vector4[4];
        }

        [Test]
        public new void ToString()
        {
            var vector = new Vector4(1.1f, 2.2f, 3.3f, 4.4f);

            Assert.AreEqual("X: 1.1 Y: 2.2 Z: 3.3 W: 4.4", vector.ToString());
        }
    }
}