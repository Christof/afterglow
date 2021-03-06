using MbUnit.Framework;

namespace Afterglow.Math
{
    public class Test_MathExtensions
    {
        [Test]
        public void EqualsWithDelta_returns_true_for_same_values()
        {
            Assert.IsTrue(5.0f.EqualsWithDelta(5.0f, Constants.DELTA));
        }

        [Test]
        public void EqualsWithDelta_returns_true_for_value_which_is_a_bit_larger()
        {
            Assert.IsTrue(5.0f.EqualsWithDelta(5.0f + Constants.DELTA, Constants.DELTA));
        }

        [Test]
        public void EqualsWithDelta_returns_true_for_value_which_is_a_bit_smaller()
        {
            Assert.IsTrue(5.0f.EqualsWithDelta(5.0f - Constants.DELTA, Constants.DELTA));
        }

        [Test]
        public void EqualsWithDelta_returns_false_for_value_outside_delta()
        {
            Assert.IsFalse(5.0f.EqualsWithDelta(7.0f));
        }

        [Test]
        public void Clamp_for_value_in_the_range_returns_the_value()
        {
            float value = 5.0f;

            Assert.AreEqual(5.0f, value.Clamp(0.0f, 10.0f));
        }

        [Test]
        public void Clamp_set_too_large_value_to_max()
        {
            float value = 5.0f;

            Assert.AreEqual(2.0f, value.Clamp(0.0f, 2.0f));
        }

        [Test]
        public void Clamp_set_too_small_value_to_min()
        {
            float value = -5.0f;

            Assert.AreEqual(0.0f, value.Clamp(0.0f, 10.0f));
        }

        [Test]
        public void ClampMax_returns_value_if_not_over_max()
        {
            float value = 10.0f;

            Assert.AreEqual(10.0f, value.ClampMax(15.0f));
        }

        [Test]
        public void ClampMax_sets_too_large_value_to_max()
        {
            float value = 15.0f;

            Assert.AreEqual(10.0f, value.ClampMax(10.0f));
        }

        [Test]
        public void ClampMin_returns_value_if_not_under_min()
        {
            float value = 10.0f;

            Assert.AreEqual(10.0f, value.ClampMin(5.0f));
        }

        [Test]
        public void ClampMin_sets_too_small_value_to_min()
        {
            float value = 5.0f;

            Assert.AreEqual(10.0f, value.ClampMin(10.0f));
        }
    }
}