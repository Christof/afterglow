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

        [Test]
        public void ToMatrix()
        {
            Vector3 rotationAxis = Vector3.ZAxis;
            var angle = Constants.HALF_PI;

            var quaternion = new Quaternion(rotationAxis, angle);

            Matrix matrix = quaternion.ToMatrix();

            var expected = new Matrix(
                Functions.Cos(angle), -Functions.Sin(angle), 0, 0,
                Functions.Sin(angle), Functions.Cos(angle), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

            matrix.ShouldEqual(expected);
        }

        [Test]
        public void Concatenation_with_the_multiply_operator()
        {
            var quat1 = new Quaternion(Vector3.ZAxis, Constants.HALF_PI);
            var quat2 = new Quaternion(Vector3.XAxis, Constants.HALF_PI);

            var concatenated = quat2 * quat1;

            concatenated.X.ShouldEqual(0.2580212f, Constants.DELTA);
            concatenated.Y.ShouldEqual(-0.2580212f, Constants.DELTA);
            concatenated.Z.ShouldEqual(0.2580212f, Constants.DELTA);
            concatenated.W.ShouldEqual(0.9689124f, Constants.DELTA);
        }
    }
}