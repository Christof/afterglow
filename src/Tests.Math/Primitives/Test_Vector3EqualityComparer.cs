using MbUnit.Framework;

namespace TheNewEngine.Math
{
    public class Test_Vector3EqualityComparer
    {
        [Test]
        public void Compare()
        {
            var comparer = new Vector3EqualityComparer();

            Assert.AreEqual(Vector3.Zero, new Vector3(float.Epsilon, 0, 0), comparer);
        }
    }
}