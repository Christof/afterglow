using System;
using MbUnit.Framework;
using Gallio.Framework.Assertions;

namespace TheNewEngine.Infrastructure
{
    public class Test_BddExtensions
    {
        [Test]
        public void ShouldNotBeNull_throws_exception_for_null_values()
        {
            object value = null;

            bool threwException = false;
            try
            {
                value.ShouldNotBeNull();
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void ShouldNotBeNull_does_nothing_for_none_values()
        {
            var value = new Exception();

            value.ShouldNotBeNull();
        }

        [Test]
        public void ShouldNotBeNull_returns_the_value_for_further_expectations()
        {
            var value = new Exception();

            var returnedValue = value.ShouldNotBeNull();

            Assert.IsNotNull(returnedValue);
        }

        [Test]
        public void ShouldBeNull_throws_exception_for_none_null_values()
        {
            var value = new Exception();

            bool threwException = false;
            try
            {
                value.ShouldBeNull();
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void ShouldBeNull_does_nothing_for_null_values()
        {
            object value = null;

            value.ShouldBeNull();
        }

        [Test]
        public void ShouldEqual_does_nothing_for_equal_values()
        {
            1.ShouldEqual(1);
        }

        [Test]
        public void ShouldEqual_throws_exception_for_different_values()
        {
            bool threwException = false;
            try
            {
                1.ShouldEqual(2);
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void ShouldEqual_with_delta_throws_exception_for_very_different_values()
        {
            bool threwException = false;
            try
            {
                1.0f.ShouldEqual(20.0f, 1.0f);
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void ShouldEqual_with_delta_does_nothing_for_nearly_the_same_values()
        {
            1.0f.ShouldEqual(1.1f, 0.2f);
        }

        [Test]
        public void ShouldNotEqual_does_nothing_for_different_values()
        {
            1.ShouldNotEqual(2);
        }

        [Test]
        public void ShouldNotEqual_throws_exception_for_same_values()
        {
            bool threwException = false;
            try
            {
                1.ShouldNotEqual(1);
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void ShouldNotEqual_returns_the_value_for_further_expectations()
        {
            1.ShouldNotEqual(2).ShouldNotEqual(3);
        }

        [Test]
        public void ShouldBeTheSameAs_throws_exception_if_the_values_are_not_the_same_instance()
        {
            bool threwException = false;
            try
            {
                new Exception().ShouldBeTheSameAs(new Exception());
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void ShouldBeTheSameAs_does_nothing_for_same_instance()
        {
            var exception = new Exception();

            exception.ShouldBeTheSameAs(exception);
        }

        [Test]
        public void ShouldNotBeTheSameAs_thorws_exception_for_same_instance()
        {
            bool threwException = false;
            try
            {
                var exception = new Exception();
                exception.ShouldNotBeTheSameAs(exception);
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void ShouldNotBeTheSameAs_does_nothing_for_different_instances()
        {
            new Exception().ShouldNotBeTheSameAs(new Exception());
        }

        [Test]
        public void ShouldNotBeTheSameAs_returns_the_value_for_further_expectations()
        {
            var returnedValue = new Exception().ShouldNotBeTheSameAs(new Exception());

            Assert.IsNotNull(returnedValue);
        }

        [Test]
        public void ShouldBeTrue_does_nothing_for_true_values()
        {
            true.ShouldBeTrue();
        }

        [Test]
        public void ShouldBeTrue_throws_exception_for_false_values()
        {
            bool threwException = false;
            try
            {
                false.ShouldBeTrue();
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void ShouldBeFalse_does_nothing_for_false_values()
        {
            false.ShouldBeFalse();
        }

        [Test]
        public void ShouldBeFalse_throws_exception_for_true_values()
        {
            bool threwException = false;
            try
            {
                true.ShouldBeFalse();
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void GetPossibleException()
        {
            Action action = () => { throw new Exception(); };
            Assert.IsInstanceOfType(typeof (Exception), action.GetPossibleException());
        }

        [Test]
        public void GetPossibleException_returns_null_if_no_exception_was_thrown()
        {
            Action action = () => { };

            Assert.IsNull(action.GetPossibleException());
        }

        private static void MethodThatThrowsAnException()
        {
            throw new Exception();
        }

        [Test]
        public void ShouldThrow_does_nothing_if_the_right_exception_was_thrown()
        {
            The.Action(MethodThatThrowsAnException).ShouldThrow<Exception>();
        }

        [Test]
        public void ShouldThrow_throws_exception_if_the_wrong_exception_was_thrown()
        {
            bool threwException = false;
            try
            {
                The.Action(MethodThatThrowsAnException).ShouldThrow<ArgumentException>();
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        private static void MethodThatThrowsArgumentException(string argument)
        {
            throw new ArgumentException("Should not be empty", "argument");
        }

        [Test]
        public void ShouldThrow_returns_exception_to_support_further_expectations()
        {
            The.Action(() => MethodThatThrowsArgumentException(string.Empty))
                .ShouldThrow<ArgumentException>()
                .ParamName.ShouldEqual("argument");
        }

        [Test]
        public void ShouldThrow_throws_an_exception_if_no_exception_was_thrown()
        {
            bool threwException = false;
            try
            {
                The.Action(() => { }).ShouldThrow<Exception>();
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void TypeShouldBe_does_nothing_for_the_right_type()
        {
            int value = 0;

            value.TypeShouldBe<int, int>();
        }

        [Test]
        public void TypeShouldBe_on_object_does_nothing_for_the_right_type()
        {
            0.TypeShouldBe<IComparable>();
        }

        [Test]
        public void TypeShouldBe_throws_exception_for_wrong_type()
        {
            bool threwException = false;
            try
            {
                0.TypeShouldBe<string>();
            }
            catch (AssertionException)
            {
                threwException = true;
            }

            Assert.IsTrue(threwException, "Should have triggered an assertion.");
        }

        [Test]
        public void TypeShouldBe_returns_the_value_cast_to_the_expected_type()
        {
            IComparable value = 2;

            int returnedValue = value.TypeShouldBe<int>();

            Assert.AreEqual(2, returnedValue);
        }
    }
}