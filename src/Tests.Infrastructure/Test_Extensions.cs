using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;
using System;
using Moq;

namespace TheNewEngine.Infrastructure
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

            var result = numbers.IndexIsMultipleOf(2);

            var expected = new[] { 1, 3, 5 };

            Assert.IsNotNull(result);
            Assert.AreElementsEqual(expected, result);
        }

        [Test]
        public void IndexIsMultipleOf_with_offset()
        {
            var numbers = new[] { 1, 2, 3, 4, 5, 6 };

            var result = numbers.IndexIsMultipleOf(2, 1);

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
        public void DisposeIfNotNull()
        {
            var disposable = new Mock<IDisposable>();
            
            disposable.Object.DisposeIfNotNull();

            disposable.Verify(d => d.Dispose());
        }
    }
}