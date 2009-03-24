using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using System;
using Moq;

namespace Afterglow.Infrastructure
{
    public class Test_Extensions
    {
        [Test]
        [Row(false, "some string")]
        [Row(true, null)]
        [Row(true, "")]
        public void String_IsNullOrEmpty(bool expected, string value)
        {
            Assert.AreEqual(expected, value.IsNullOrEmpty());
        }

        [Test]
        public void Slice()
        {
            var numbers = new[] { 1, 2, 3, 4, 5, 6 };

            var slices = numbers.Slice(2);

            Assert.IsNotNull(slices);
            Assert.AreEqual(3, slices.Count());
            Assert.AreEqual(2, slices.ElementAt(0).Count());
        }

        [Test]
        public void IndexIsMultipleOf()
        {
            var numbers = new[] { 1, 2, 3, 4, 5, 6 };

            var result = numbers.EachNthElement(2);

            var expected = new[] { 1, 3, 5 };

            Assert.IsNotNull(result);
            Assert.AreElementsEqual(expected, result);
        }

        [Test]
        public void IndexIsMultipleOf_with_offset()
        {
            var numbers = new[] { 1, 2, 3, 4, 5, 6 };

            var result = numbers.EachNthElement(2, 1);

            var expected = new[] { 2, 4, 6 };

            Assert.IsNotNull(result);
            Assert.AreElementsEqual(expected, result);
        }

        [Test]
        public void DowncastTo()
        {
            IList<int> listInterface = new List<int>();

            List<int> downcastList = listInterface.DowncastTo<List<int>>();

            Assert.IsNotNull(downcastList);
        }

        [Test]
        public void DisposeIfNotNull_for_not_null_disposable_calls_Dispose()
        {
            var disposable = new Mock<IDisposable>();
            
            disposable.Object.DisposeIfNotNull();

            disposable.Verify(d => d.Dispose());
        }

        [Test]
        public void DisposeIfNotNull_for_null_disposable_does_not_call_Dispose()
        {
            IDisposable disposable = null;

            disposable.DisposeIfNotNull();
        }

        [Test]
        public void Foreach_calls_given_lambda_for_each_element()
        {
            var elements = new[] { 1, 2, 3 };
            int sum = 0;
            elements.ForEach(element => sum += element);

            sum.ShouldEqual(6);
        }

        [Test]
        public void TransformCollection_transforms_long_to_string_array()
        {
            var source = new[] { 1, 2, 3 };

            var destination = source.TransformCollection(number => number.ToString()).ToArray();

            destination.Length.ShouldEqual(3);
            destination[0].ShouldEqual("1");
            destination[1].ShouldEqual("2");
            destination[2].ShouldEqual("3");
        }

        [Test]
        public void TransformCollection_throws_exception_if_source_is_null()
        {
            int[] source = null;

            The.Action(
                () => source.TransformCollection(number => "dummy")
                ).ShouldThrow<ArgumentNullException>().ParamName.ShouldEqual(
                "source");
        }

        [Test]
        public void TransformCollection_throws_exception_if_tranformationOfOne_is_null()
        {
            var source = new[] { 1, 2, 3 };
            Func<int, string> logic = null;

            The.Action(
                () => source.TransformCollection(logic)
                ).ShouldThrow<ArgumentNullException>().ParamName.ShouldEqual(
                "transformationOfOne");
        }

        [Test]
        public void TransformObjectCollection_transforms_long_to_string_array()
        {
            var source = new[] { 1, 2, 3 };

            var destination = source.TransformObjectCollection(number => number.ToString()).ToArray();

            destination.Length.ShouldEqual(3);
            destination[0].ShouldEqual("1");
            destination[1].ShouldEqual("2");
            destination[2].ShouldEqual("3");
        }

        [Test]
        public void TransformObjectCollection_throws_exception_if_source_is_null()
        {
            int[] source = null;

            The.Action(
                () => source.TransformObjectCollection(number => "dummy")
                ).ShouldThrow<ArgumentNullException>().ParamName.ShouldEqual(
                "source");
        }

        [Test]
        public void TransformObjectColletion_throws_exception_if_tranformationOfOne_is_null()
        {
            var source = new[] { 1, 2, 3 };
            Func<object, string> logic = null;

            The.Action(
                () => source.TransformObjectCollection(logic)
                ).ShouldThrow<ArgumentNullException>().ParamName.ShouldEqual(
                "transformationOfOne");
        }
    }
}