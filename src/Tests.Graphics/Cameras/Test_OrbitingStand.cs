using Afterglow.Math;
using MbUnit.Framework;
using System;

namespace Afterglow.Graphics.Cameras
{
    public class Test_OrbitingStand
    {
        [Test]
        public void Constructor_initializes_radius_azimuth_and_declination()
        {
            var orbitingStand = new OrbitingStand();

            Assert.IsNotNull(orbitingStand);
            Assert.AreEqual(10.0f, orbitingStand.Radius);
            Assert.AreEqual(0.0f, orbitingStand.Azimuth);
            Assert.AreEqual(0.0f, orbitingStand.Declination);
        }

        [Test]
        public void Constructor_sets_radius_azimuth_and_declination()
        {
            var radius = 20.0f;
            var azimuth = Constants.PI;
            var declination = Constants.HALF_PI * 0.5f;

            var orbitingStand = new OrbitingStand(radius, azimuth, declination);

            Assert.IsNotNull(orbitingStand);
            Assert.AreEqual(radius, orbitingStand.Radius);
            Assert.AreEqual(azimuth, orbitingStand.Azimuth);
            Assert.AreEqual(declination, orbitingStand.Declination);
        }

        [Test]
        public void Position_and_Direction_only_with_radius()
        {
            var orbitingStand = new OrbitingStand();

            Assert.AreEqual(new Vector3(10, 0, 0), orbitingStand.Position);
            Assert.AreEqual(new Vector3(-1, 0, 0), orbitingStand.Direction,
                new Vector3EqualityComparer());
        }

        [Test]
        public void Position_and_Direction_with_radius_and_azimuth()
        {
            var orbitingStand = new OrbitingStand(10, Constants.HALF_PI, 0);

            Assert.AreEqual(new Vector3(0, 0, 10), orbitingStand.Position,
                new Vector3EqualityComparer());
            Assert.AreEqual(new Vector3(0, 0, -1), orbitingStand.Direction,
                new Vector3EqualityComparer());
        }

        [Test]
        public void Position_and_Direction_with_radius_and_declination()
        {
            var orbitingStand = new OrbitingStand(10, 0, Constants.HALF_PI * 0.5f);

            var expectedPosition = new Vector3(10 / Functions.Sqrt(2), 10 / Functions.Sqrt(2), 0);
            Assert.AreEqual(expectedPosition,
                orbitingStand.Position, new Vector3EqualityComparer());
            Assert.AreEqual(-expectedPosition.Normalized(), orbitingStand.Direction,
                new Vector3EqualityComparer());
        }

        [Test]
        public void Position_and_Direction_with_radius_and_declination_for_z_axis()
        {
            var orbitingStand = new OrbitingStand(10, 0, Constants.HALF_PI * 0.5f);

            var expectedPosition = new Vector3(10 / Functions.Sqrt(2), 10 / Functions.Sqrt(2), 0);
            Assert.AreEqual(expectedPosition, orbitingStand.Position,
                new Vector3EqualityComparer());
            Assert.AreEqual(-expectedPosition.Normalized(), orbitingStand.Direction,
                new Vector3EqualityComparer());
        }

        [Test]
        public void Position_and_Direction_with_radius_azimuth_and_declination()
        {
            var orbitingStand = new OrbitingStand(
                10, Constants.PI * 0.75f, Constants.HALF_PI * 0.5f);

            var expectedPosition = new Vector3(-5, 7.071068f, 5);
            Assert.AreEqual(expectedPosition, orbitingStand.Position, 
                new Vector3EqualityComparer());
            Assert.AreEqual(-expectedPosition.Normalized(), orbitingStand.Direction,
                new Vector3EqualityComparer());
        }

        [Test]
        public void MaxDeclination_is_limited_to_half_pi()
        {
            var orbitingStand = new OrbitingStand();

            orbitingStand.MaxDeclination = Constants.PI;

            Assert.AreApproximatelyEqual(Constants.HALF_PI, orbitingStand.MaxDeclination,
                2 * Constants.DELTA);
        }

        [Test]
        public void MinDeclination_is_limited_to_minus_half_pi()
        {
            var orbitingStand = new OrbitingStand();

            orbitingStand.MinDeclination = -Constants.PI;

            Assert.AreApproximatelyEqual(-Constants.HALF_PI, orbitingStand.MinDeclination,
                2 * Constants.DELTA);
        }

        [Test]
        public void ViewMatrix()
        {
            var orbitingStand = new OrbitingStand();

            var viewMatrix = orbitingStand.ViewMatrix;

            Assert.AreNotEqual(new Matrix(), viewMatrix);
            Assert.AreNotEqual(Matrix.Identity, viewMatrix);
        }

        [Test]
        public void UpVector_is_y_axis()
        {
            var orbitingStand = new OrbitingStand();

            Assert.AreEqual(Vector3.YAxis, orbitingStand.Up);
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void Position_setter_is_not_supported_yet()
        {
            var orbitingStand = new OrbitingStand();

            orbitingStand.Position = new Vector3();
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void Direction_setter_is_not_supported_yet()
        {
            var orbitingStand = new OrbitingStand();

            orbitingStand.Direction = new Vector3();
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void Up_setter_is_not_supported_yet()
        {
            var orbitingStand = new OrbitingStand();

            orbitingStand.Up = new Vector3();
        }
    }
}