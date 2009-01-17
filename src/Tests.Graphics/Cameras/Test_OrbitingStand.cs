using MbUnit.Framework;
using TheNewEngine.Math;

namespace TheNewEngine.Graphics.Cameras
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
        public void Position_only_with_radius()
        {
            var orbitingStand = new OrbitingStand();

            Assert.AreEqual(new Vector3(10, 0, 0), orbitingStand.Position);
        }

        [Test]
        public void Position_with_radius_and_azimuth()
        {
            var orbitingStand = new OrbitingStand(10, Constants.HALF_PI, 0);

            Assert.AreEqual(new Vector3(0, 0, 10), orbitingStand.Position,
                new Vector3EqualityComparer());
        }

        [Test]
        public void Position_with_radius_and_declination()
        {
            var orbitingStand = new OrbitingStand(10, 0, Constants.HALF_PI);

            Assert.AreEqual(new Vector3(0, 10, 0), orbitingStand.Position,
                new Vector3EqualityComparer());
        }

        [Test]
        public void Position_with_radius_and_declination_for_z_axis()
        {
            var orbitingStand = new OrbitingStand(10, 0, Constants.HALF_PI * 0.5f);

            Assert.AreEqual(new Vector3(10 / Functions.Sqrt(2), 10 / Functions.Sqrt(2), 0), orbitingStand.Position,
                new Vector3EqualityComparer());
        }

        [Test]
        public void Position_with_radius_azimuth_and_declination()
        {
            var orbitingStand = new OrbitingStand(
                10, Constants.PI * 0.75f, Constants.HALF_PI * 0.5f);

            Assert.AreEqual(new Vector3(-5, 7.071068f, 5), orbitingStand.Position,
                new Vector3EqualityComparer());
        }
    }
}