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

        [Test]
        public void Constructor_with_rotation_axis_and_angle()
        {
            Vector3 rotationAxis = Vector3.ZAxis;
            var angle = Constants.HALF_PI;

            var quaternion = new Quaternion(rotationAxis, angle);

            var halfSquarerootOfTwo = 0.5f * Functions.Sqrt(2);

            quaternion.W.ShouldEqual(halfSquarerootOfTwo);
            quaternion.X.ShouldEqual(0);
            quaternion.Y.ShouldEqual(0);
            quaternion.Z.ShouldEqual(halfSquarerootOfTwo);
        }

        [Test]
        public void Constructor_from_a_vector_sets_XYZ_to_the_vector_values_and_W_to_0()
        {
            var vector3 = new Vector3(1, 2, 3);

            var quaternion = new Quaternion(vector3);

            quaternion.W.ShouldEqual(0);
            quaternion.X.ShouldEqual(vector3.X);
            quaternion.Y.ShouldEqual(vector3.Y);
            quaternion.Z.ShouldEqual(vector3.Z);
        }

        [Test]
        public void ToAxisAngle()
        {
            Vector3 rotationAxis = Vector3.ZAxis;
            var angle = Constants.HALF_PI;

            var quaternion = new Quaternion(rotationAxis, angle);

            Vector4 axisAngle = quaternion.ToAxisAngle();

            axisAngle.X.ShouldEqual(rotationAxis.X);
            axisAngle.Y.ShouldEqual(rotationAxis.Y);
            axisAngle.Z.ShouldEqual(rotationAxis.Z);
            axisAngle.W.ShouldEqual(angle);
        }
    }
}