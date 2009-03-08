using Afterglow.Infrastructure;
using MbUnit.Framework;

namespace Afterglow.Math
{
    public class Test_Quaternion
    {
        [Test]
        public void Constructor_with_two_vectors_creates_rotation_between_the_two()
        {
            Vector3 sourcePosition = Vector3.XAxis;
            Vector3 destinationPosition = Vector3.YAxis;

            var quaternion = new Quaternion(sourcePosition, destinationPosition);

            var halfSquarerootOfTwo = 0.5f * Functions.Sqrt(2);
            quaternion.W.ShouldEqual(halfSquarerootOfTwo);
            quaternion.X.ShouldEqual(0);
            quaternion.Y.ShouldEqual(0);
            quaternion.Z.ShouldEqual(halfSquarerootOfTwo);
        }
    }
}