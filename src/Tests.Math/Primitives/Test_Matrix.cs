using MbUnit.Framework;

namespace TheNewEngine.Math.Primitives
{
    public class Test_Matrix
    {
        [Test]
        public void Constructor_initializes_with_rows_and_access_by_rows()
        {
            var firstRow = new Vector4(11, 12, 13, 14);
            var secondRow = new Vector4(21, 22, 23, 24);
            var thirdRow = new Vector4(31, 32, 33, 34);
            var fourthRow = new Vector4(41, 42, 43, 44);

            var matrix = new Matrix(firstRow, secondRow, thirdRow, fourthRow);

            Assert.AreEqual(firstRow, matrix.FirstRow);
            Assert.AreEqual(secondRow, matrix.SecondRow);
            Assert.AreEqual(thirdRow, matrix.ThirdRow);
            Assert.AreEqual(fourthRow, matrix.FourthRow);
        }

        [Test]
        public void Constructor_intialize_with_values_and_access_by_values()
        {
            float r1c1 = 11;
            float r1c2 = 12;
            float r1c3 = 13;
            float r1c4 = 14;

            float r2c1 = 21;
            float r2c2 = 22;
            float r2c3 = 23;
            float r2c4 = 24;

            float r3c1 = 31;
            float r3c2 = 32;
            float r3c3 = 33;
            float r3c4 = 34;

            float r4c1 = 41;
            float r4c2 = 42;
            float r4c3 = 43;
            float r4c4 = 44;

            var matrix = new Matrix(
                r1c1, r1c2, r1c3, r1c4,
                r2c1, r2c2, r2c3, r2c4,
                r3c1, r3c2, r3c3, r3c4,
                r4c1, r4c2, r4c3, r4c4);
        }

        [Test]
        public void Test()
        {
//            var matrix = new Matrix();
//
//            matrix.FirstRow.Z = 1.0f;
//
//            Assert.AreEqual(1.0f, matrix.FirstRow.X);
        }
    }
}