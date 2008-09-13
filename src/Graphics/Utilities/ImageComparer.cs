using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Linq;

namespace TheNewEngine.Graphics.Utilities
{
    /// <summary>
    /// Compares images either in memory or from the hard disk.
    /// </summary>
    public static class ImageComparer
    {
        /// <summary>
        /// Compares the given images for equality.
        /// </summary>
        /// <param name="firstImagePath">The first image path.</param>
        /// <param name="secondImagePath">The second image path.</param>
        /// <returns><c>true</c> if the images are equal (per pixel); otherwise <c>false</c>.</returns>
        public static bool Compare(string firstImagePath, string secondImagePath)
        {
            if (!File.Exists(firstImagePath))
            {
                return false;
            }

            if (!File.Exists(secondImagePath))
            {
                return false;
            }

            if (new FileInfo(firstImagePath).Length != new FileInfo(secondImagePath).Length)
            {
                return false;
            }

            return Compare(new Bitmap(firstImagePath), new Bitmap(secondImagePath));
        }

        /// <summary>
        /// Compares the given bitmap instances.
        /// </summary>
        /// <param name="firstBitmap">The first bitmap.</param>
        /// <param name="secondBitmap">The second bitmap.</param>
        /// <returns><c>true</c> if the images are equal (with hash); otherwise <c>false</c>.</returns>
        public static bool Compare(Bitmap firstBitmap, Bitmap secondBitmap)
        {
            if (firstBitmap.Size != secondBitmap.Size)
            {
                return false;
            }

            var imageConverter = new ImageConverter();
            var bytesOfFirstBitmap = imageConverter.ConvertTo<byte[]>(firstBitmap);
            var bytesOfSecondBitmap = imageConverter.ConvertTo<byte[]>(secondBitmap);

            // the byte arrays are hashed and then compared.
            // TODO (christof sirk) run performance tests to see if this is really faster
            // than comparing the bytes. Maybe use the parallel extension to use more cores.
            var sha256Managed = new SHA256Managed();
            byte[] firstHash = sha256Managed.ComputeHash(bytesOfFirstBitmap);
            byte[] secondHash = sha256Managed.ComputeHash(bytesOfSecondBitmap);

            return firstHash.SequenceEqual(secondHash);
        }

        /// <summary>
        /// Compares the two given bitmaps per byte.
        /// </summary>
        /// <param name="firstBitmap">The first bitmap.</param>
        /// <param name="secondBitmap">The second bitmap.</param>
        /// <returns><c>true</c> if the images are equal (per byte); otherwise <c>false</c>.</returns>
        public static bool ComparePerByte(Bitmap firstBitmap, Bitmap secondBitmap)
        {
            if (firstBitmap.Size != secondBitmap.Size)
            {
                return false;
            }

            var imageConverter = new ImageConverter();
            var bytesOfFirstBitmap = imageConverter.ConvertTo<byte[]>(firstBitmap);
            var bytesOfSecondBitmap = imageConverter.ConvertTo<byte[]>(secondBitmap);

            return bytesOfFirstBitmap.SequenceEqual(bytesOfSecondBitmap);
        }

        /// <summary>
        /// Compares the two given bitmaps per pixel.
        /// </summary>
        /// <param name="firstBitmap">The first bitmap.</param>
        /// <param name="secondBitmap">The second bitmap.</param>
        /// <returns><c>true</c> if the images are equal (per pixel); otherwise <c>false</c>.</returns>
        public static bool ComparePerPixel(Bitmap firstBitmap, Bitmap secondBitmap)
        {
            if (firstBitmap.Size != secondBitmap.Size)
            {
                return false;
            }

            for (int i = 0; i < firstBitmap.Size.Height; i++)
            {
                for (int j = 0; j < firstBitmap.Size.Width; j++)
                {
                    var firstColor = firstBitmap.GetPixel(j, i);
                    var secondColor = secondBitmap.GetPixel(j, i);

                    if (firstColor != secondColor)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Converts the given object to the specified generic type.
        /// </summary>
        /// <typeparam name="T">Target type of the conversions.</typeparam>
        /// <param name="imageConverter">The image converter.</param>
        /// <param name="value">The value.</param>
        /// <returns>The converted value.</returns>
        private static T ConvertTo<T>(this ImageConverter imageConverter, object value)
        {
            return (T) imageConverter.ConvertTo(value, typeof (T));
        }
    }
}