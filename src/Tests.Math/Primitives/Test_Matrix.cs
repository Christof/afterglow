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
    }
}