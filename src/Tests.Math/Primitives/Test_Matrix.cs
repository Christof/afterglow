using MbUnit.Framework;

namespace TheNewEngine.Math.Primitives
{
    public class Test_Matrix
    {
        [Test]
        public void Constructor_intialize_with_values_and_access_by_values()
        {
            var r1c1 = 11;
            var r1c2 = 12;
            var r1c3 = 13;
            var r1c4 = 14;

            var r2c1 = 21;
            var r2c2 = 22;
            var r2c3 = 23;
            var r2c4 = 24;

            var r3c1 = 31;
            var r3c2 = 32;
            var r3c3 = 33;
            var r3c4 = 34;

            var r4c1 = 41;
            var r4c2 = 42;
            var r4c3 = 43;
            var r4c4 = 44;

            var matrix = new Matrix(
                r1c1, r1c2, r1c3, r1c4,
                r2c1, r2c2, r2c3, r2c4,
                r3c1, r3c2, r3c3, r3c4,
                r4c1, r4c2, r4c3, r4c4);

            Assert.AreEqual(r1c1, matrix.R1C1);
            Assert.AreEqual(r1c2, matrix.R1C2);
            Assert.AreEqual(r1c3, matrix.R1C3);
            Assert.AreEqual(r1c4, matrix.R1C4);

            Assert.AreEqual(r2c1, matrix.R2C1);
            Assert.AreEqual(r2c2, matrix.R2C2);
            Assert.AreEqual(r2c3, matrix.R2C3);
            Assert.AreEqual(r2c4, matrix.R2C4);

            Assert.AreEqual(r3c1, matrix.R3C1);
            Assert.AreEqual(r3c2, matrix.R3C2);
            Assert.AreEqual(r3c3, matrix.R3C3);
            Assert.AreEqual(r3c4, matrix.R3C4);
            
            Assert.AreEqual(r4c1, matrix.R4C1);
            Assert.AreEqual(r4c2, matrix.R4C2);
            Assert.AreEqual(r4c3, matrix.R4C3);
            Assert.AreEqual(r4c4, matrix.R4C4);
        }

        [Test]
        public void Properties()
        {
            var r1c1 = 11;
            var r1c2 = 12;
            var r1c3 = 13;
            var r1c4 = 14;

            var r2c1 = 21;
            var r2c2 = 22;
            var r2c3 = 23;
            var r2c4 = 24;

            var r3c1 = 31;
            var r3c2 = 32;
            var r3c3 = 33;
            var r3c4 = 34;

            var r4c1 = 41;
            var r4c2 = 42;
            var r4c3 = 43;
            var r4c4 = 44;

            var matrix = new Matrix();

            matrix.R1C1 = r1c1;
            matrix.R1C2 = r1c2;
            matrix.R1C3 = r1c3;
            matrix.R1C4 = r1c4;

            matrix.R2C1 = r2c1;
            matrix.R2C2 = r2c2;
            matrix.R2C3 = r2c3;
            matrix.R2C4 = r2c4;

            matrix.R3C1 = r3c1;
            matrix.R3C2 = r3c2;
            matrix.R3C3 = r3c3;
            matrix.R3C4 = r3c4;

            matrix.R4C1 = r4c1;
            matrix.R4C2 = r4c2;
            matrix.R4C3 = r4c3;
            matrix.R4C4 = r4c4;

            Assert.AreEqual(r1c1, matrix.R1C1);
            Assert.AreEqual(r1c2, matrix.R1C2);
            Assert.AreEqual(r1c3, matrix.R1C3);
            Assert.AreEqual(r1c4, matrix.R1C4);

            Assert.AreEqual(r2c1, matrix.R2C1);
            Assert.AreEqual(r2c2, matrix.R2C2);
            Assert.AreEqual(r2c3, matrix.R2C3);
            Assert.AreEqual(r2c4, matrix.R2C4);

            Assert.AreEqual(r3c1, matrix.R3C1);
            Assert.AreEqual(r3c2, matrix.R3C2);
            Assert.AreEqual(r3c3, matrix.R3C3);
            Assert.AreEqual(r3c4, matrix.R3C4);

            Assert.AreEqual(r4c1, matrix.R4C1);
            Assert.AreEqual(r4c2, matrix.R4C2);
            Assert.AreEqual(r4c3, matrix.R4C3);
            Assert.AreEqual(r4c4, matrix.R4C4);
        }

        [Test]
        public void Identity()
        {
            var matrix = Matrix.Identity;

            Assert.AreEqual(1.0f, matrix.R1C1);
            Assert.AreEqual(0.0f, matrix.R1C2);
            Assert.AreEqual(0.0f, matrix.R1C3);
            Assert.AreEqual(0.0f, matrix.R1C4);

            Assert.AreEqual(0.0f, matrix.R2C1);
            Assert.AreEqual(1.0f, matrix.R2C2);
            Assert.AreEqual(0.0f, matrix.R2C3);
            Assert.AreEqual(0.0f, matrix.R2C4);

            Assert.AreEqual(0.0f, matrix.R3C1);
            Assert.AreEqual(0.0f, matrix.R3C2);
            Assert.AreEqual(1.0f, matrix.R3C3);
            Assert.AreEqual(0.0f, matrix.R3C4);

            Assert.AreEqual(0.0f, matrix.R4C1);
            Assert.AreEqual(0.0f, matrix.R4C2);
            Assert.AreEqual(0.0f, matrix.R4C3);
            Assert.AreEqual(1.0f, matrix.R4C4);
        }

        [Test]
        public void Implements_ICoordinateSystem_XAxis()
        {
            ICoordinateSystem matrix = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            Vector3 xAxis = matrix.XAxis;

            Assert.AreEqual(new Vector3(11, 21, 31), xAxis);
        }

        [Test]
        public void Implements_ICoordinateSystem_YAxis()
        {
            ICoordinateSystem matrix = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            Vector3 yAxis = matrix.YAxis;

            Assert.AreEqual(new Vector3(12, 22, 32), yAxis);
        }

        [Test]
        public void Implements_ICoordinateSystem_ZAxis()
        {
            ICoordinateSystem matrix = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            Vector3 zAxis = matrix.ZAxis;

            Assert.AreEqual(new Vector3(13, 23, 33), zAxis);
        }

        [Test]
        public void Implements_ICoordinateSystem_Origin()
        {
            ICoordinateSystem matrix = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            Vector3 origin = matrix.Origin;

            Assert.AreEqual(new Vector3(14, 24, 34), origin);
        }

        [Test]
        public void Transpose()
        {
            var matrix = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            var expected = new Matrix(
                11, 21, 31, 41,
                12, 22, 32, 42,
                13, 23, 33, 43,
                14, 24, 34, 44);

            matrix.Transpose();
            
            Assert.AreEqual(expected, matrix);
        }

        [Test]
        public void Multiplication_operator()
        {
            var matrix1 = new Matrix(
                11, 12, 13, 14,
                21, 22, 23, 24,
                31, 32, 33, 34,
                41, 42, 43, 44);

            var matrix2 = new Matrix(
                55, 56, 57, 58,
                65, 66, 67, 68,
                75, 76, 77, 78,
                85, 86, 87, 88);

            var expected = new Matrix(
                3550, 3600, 3650, 3700,
                6350, 6440, 6530, 6620,
                9150, 9280, 9410, 9540,
                11950, 12120, 12290, 12460);

            var multiplied = matrix1 * matrix2;

            Assert.AreEqual(expected, multiplied);
        }
    }
}