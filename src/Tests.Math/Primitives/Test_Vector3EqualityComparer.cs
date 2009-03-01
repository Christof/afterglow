using MbUnit.Framework;

namespace Afterglow.Math
{
    public class Test_Vector3EqualityComparer
    {
        [Test]
        public void Compare()
        {
            var comparer = new Vector3EqualityComparer();

            Assert.AreEqual(Vector3.Zero, new Vector3(float.Epsilon, 0, 0), comparer);
        }

        [Test]
        public void GetHashCode_returns_hashcode_of_given_object()
        {
            var comparer = new Vector3EqualityComparer();

            var vector = new Vector3(1, 2, 3);
            Assert.AreEqual(vector.GetHashCode(), comparer.GetHashCode(vector));
        }
    }
}