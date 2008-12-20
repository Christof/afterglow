using MbUnit.Framework;
using System;

namespace TheNewEngine.Math
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
        public void Zero()
        {
            var zero = Vector3.Zero;

            Assert.AreEqual(new Vector3(0, 0, 0), zero);
        }

        [Test]
        public void XAxis()
        {
            var xAxis = Vector3.XAxis;

            Assert.AreEqual(new Vector3(1, 0, 0), xAxis);
        }

        [Test]
        public void YAxis()
        {
            var yAxis = Vector3.YAxis;

            Assert.AreEqual(new Vector3(0, 1, 0), yAxis);
        }

        [Test]
        public void ZAxis()
        {
            var zAxis = Vector3.ZAxis;

            Assert.AreEqual(new Vector3(0, 0, 1), zAxis);
        }

        [Test]
        public void Access_values_by_index()
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
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Access_values_by_index_throws_OutOfRangeException_if_index_is_out_of_range()
        {
            var vector3 = new Vector3();

            var value = vector3[3];
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
        public void Normalized()
        {
            var original = new Vector3(1, 3, 5);

            var normalized = original.Normalized();

            var expected = new Vector3(original.X / original.Length, 
                original.Y / original.Length, original.Z / original.Length);

            Assert.AreEqual(expected, normalized);
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

        [Test]
        public void Dot()
        {
            var vector1 = new Vector3(2.0f, 3.0f, 4.0f);
            var vector2 = new Vector3(5.0f, 6.0f, 7.0f);

            var result = vector1.Dot(vector2);

            Assert.AreEqual(2 * 5 + 3 * 6 + 4 * 7, result);
        }

        [Test]
        public void Plus_Operator()
        {
            var vector1 = new Vector3(2.0f, 3.0f, 4.0f);
            var vector2 = new Vector3(5.0f, 6.0f, 7.0f);

            var result = vector1 + vector2;

            Assert.AreEqual(vector1.X + vector2.X, result.X);
            Assert.AreEqual(vector1.Y + vector2.Y, result.Y);
            Assert.AreEqual(vector1.Z + vector2.Z, result.Z);
        }

        [Test]
        public void Minus_Operator()
        {
            var vector1 = new Vector3(2.0f, 3.0f, 4.0f);
            var vector2 = new Vector3(5.0f, 6.0f, 7.0f);

            var result = vector1 - vector2;

            Assert.AreEqual(vector1.X - vector2.X, result.X);
            Assert.AreEqual(vector1.Y - vector2.Y, result.Y);
            Assert.AreEqual(vector1.Z - vector2.Z, result.Z);
        }

        [Test]
        public void Unary_Minus_Operator()
        {
            var vector = new Vector3(2.0f, 3.0f, 4.0f);

            var result = -vector;

            Assert.AreEqual(-vector.X, result.X);
            Assert.AreEqual(-vector.Y, result.Y);
            Assert.AreEqual(-vector.Z, result.Z);
        }

        [Test]
        public void Multiplication_with_scalar_operator()
        {
            var vector = new Vector3(2.0f, 3.0f, 4.0f);

            var result = 2.0f * vector;

            Assert.AreEqual(2.0f * vector.X, result.X);
            Assert.AreEqual(2.0f * vector.Y, result.Y);
            Assert.AreEqual(2.0f * vector.Z, result.Z);
        }

        [Test]
        public new void ToString()
        {
            var vector = new Vector3(1.1f, 2.2f, 3.3f);

            Assert.AreEqual("X: 1.1 Y: 2.2 Z: 3.3", vector.ToString());
        }
    }
}