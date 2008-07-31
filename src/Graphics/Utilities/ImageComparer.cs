using System.Drawing;
using System.IO;
using System.Security.Cryptography;

namespace TheNewEngine.Graphics.Utilities
{
    public static class ImageComparer
    {
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

        public static bool Compare(Bitmap firstBitmap, Bitmap secondBitmap)
        {
            if (firstBitmap.Size != secondBitmap.Size)
            {
                return false;
            }

            var imageConverter = new ImageConverter();
            var bytesOfFirstBitmap = imageConverter.ConvertTo<byte[]>(firstBitmap);
            var bytesOfSecondBitmap = imageConverter.ConvertTo<byte[]>(secondBitmap);

            var sha256Managed = new SHA256Managed();
            byte[] firstHash = sha256Managed.ComputeHash(bytesOfFirstBitmap);
            byte[] secondHash = sha256Managed.ComputeHash(bytesOfSecondBitmap);

            for (int i = 0; i < firstHash.Length; i++)
            {
                if (firstHash[i] != secondHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static T ConvertTo<T>(this ImageConverter imageConverter, object value)
        {
            return (T) imageConverter.ConvertTo(value, typeof (T));
        }
    }
}