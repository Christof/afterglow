using MbUnit.Framework;
using System.IO;

namespace TheNewEngine.Infrastructure
{
    public class Test_Serializer
    {
        private const string FILENAME = "foo.bin";

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(FILENAME))
            {
                File.Delete(FILENAME);
            }
        }

        [Test]
        public void SerializeBinary_and_DeserializeBinary()
        {
            var value = 12;
            Serializer.SerializeBinary(value, FILENAME);

            Serializer.DeserializeBinary<int>(FILENAME).ShouldEqual(value);
        }
    }
}