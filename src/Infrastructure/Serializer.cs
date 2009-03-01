using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Afterglow.Infrastructure
{
    /// <summary>
    /// Contains helper methods to serialize and deserialize objects.
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Serializes the given object with the binary formatter into a file given by the file name.
        /// </summary>
        /// <typeparam name="T">Type of the object to be serialize</typeparam>
        /// <param name="toSerialize">Object to be serialized.</param>
        /// <param name="filename">The filename.</param>
        public static void SerializeBinary<T>(T toSerialize, string filename)
        {
            using (var stream = File.Create(filename))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, toSerialize);
            }
        }

        /// <summary>
        /// Deserializes the object contained in the file given by the file name.
        /// </summary>
        /// <typeparam name="T">Type of the serialized object.</typeparam>
        /// <param name="filename">The filename.</param>
        /// <returns>The deserailized object.</returns>
        public static T DeserializeBinary<T>(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}