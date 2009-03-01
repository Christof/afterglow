using MbUnit.Framework;
using Afterglow.Infrastructure;

namespace Afterglow.Math
{
    public class Test_Functions
    {
        private const float DELTA = 0.000001f;

        private const float PI = (float)System.Math.PI;

        private const float PI_HALF = PI * 0.5f;

        private const float PI_QUATER = PI * 0.25f;

        [Test]
        [Row(2, 4)]
        [Row(3, 9)]
        public void Sqrt(float squareRoot, float value)
        {
            Functions.Sqrt(value).ShouldEqual(squareRoot);
        }

        [Test]
        [Row(8, 2, 3)]
        [Row(1, 3, 0)]
        public void Pow(float result, float baseValue, float exponent)
        {
            Functions.Pow(baseValue, exponent).ShouldEqual(result);
        }

        [Test]
        [Row(0, 0)]
        [Row(1, PI_HALF)]
        [Row(0, PI)]
        [Row(-1, PI_HALF * 3)]
        public void Sin(float sin, float angle)
        {
            Functions.Sin(angle).ShouldEqual(sin, DELTA);
        }

        [Test]
        [Row(1, 0)]
        [Row(0, PI_HALF)]
        [Row(-1, PI)]
        [Row(0, PI_HALF * 3)]
        public void Cos(float cos, float angle)
        {
            Functions.Cos(angle).ShouldEqual(cos, DELTA);
        }

        [Test]
        [Row(0, 0)]
        [Row(1, PI_QUATER)]
        [Row(0, PI)]
        public void Tan(float tan, float angle)
        {
            Functions.Tan(angle).ShouldEqual(tan, DELTA);
        }

        [Test]
        [Row(0.0f, PI_HALF)]
        [Row(1.0f, PI_QUATER)]
        [Row(float.PositiveInfinity, 0.0f)]
        public void CoTan(float coTan, float angle)
        {
            Functions.CoTan(angle).ShouldEqual(coTan, DELTA);
        }

        [Test]
        [Row(0, 0)]
        [Row(PI_HALF, 1)]
        [Row(-PI_HALF, -1)]
        public void Asin(float acos, float angle)
        {
            Functions.Asin(angle).ShouldEqual(acos);
        }

        [Test]
        [Row(0, 1)]
        [Row(PI_HALF, 0)]
        [Row(PI, -1)]
        public void Acos(float acos, float angle)
        {
            Functions.Acos(angle).ShouldEqual(acos);
        }

        [Test]
        [Row(0, 0)]
        [Row(PI_QUATER, 1)]
        public void Atan(float atan, float angle)
        {
            Functions.Atan(angle).ShouldEqual(atan);
        }

        [Test]
        [Row(1.0f, 1.0f)]
        [Row(1.0f, -1.0f)]
        public void Abs(float abs, float value)
        {
            Functions.Abs(value).ShouldEqual(abs);
        }
    }
}