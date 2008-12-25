using System.Collections.Generic;
using System.Linq;
using MbUnit.Framework;

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
        public void Divide()
        {
            var numbers = new[] { 1, 2, 3, 4, 5, 6 };

            var slices = numbers.Slice(2);

            Assert.IsNotNull(slices);
            Assert.AreEqual(3, slices.Count());
            Assert.AreEqual(2, slices.ElementAt(0).Count());
        }

        [Test]
        public void DowncastTo()
        {
            IList<int> listInterface = new List<int>();

            List<int> downcastList = listInterface.DowncastTo<List<int>>();

            Assert.IsNotNull(downcastList);
        }
    }
}