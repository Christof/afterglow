using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public void ToArrayWithOneElement_int()
        {
            const int INTEGER = 3;
            var a = INTEGER.ToArrayWithOneElement();
            a.ShouldEqual(new [] { INTEGER });
        }

        [Test]
        public void ToArrayWithOneElement_null()
        {
            object o = null;
            var a = o.ToArrayWithOneElement();
            a.ShouldEqual(new object[] { null });

            var a2 = Extensions.ToArrayWithOneElement<int?>(null);
            a2.ShouldEqual(new int?[] { null });
        }

        [Test]
        public void GetCustomAttributes_test_attribute()
        {
            var attributes = MethodBase.GetCurrentMethod().GetCustomAttributes<TestAttribute>();
            attributes.Length.ShouldEqual(1);
        }

        [Test]
        public void GetCustomAttributes_empty()
        {
            var attributes = MethodBase.GetCurrentMethod().GetCustomAttributes<TestFixtureAttribute>();
            attributes.Length.ShouldEqual(0);
        }
    }
}