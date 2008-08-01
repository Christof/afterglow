using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MbUnit.Framework;

namespace TheNewEngine.Graphics.Utilities
{
    public class TestImageComparer
    {
        [Test]
        public void ProvidesEarlyOutIfTheFirstFileDoesntExist()
        {
            Assert.IsFalse(ImageComparer.Compare("none existing file", "doesnt matter"));
        }

        [Test]
        public void ProvidesEarlyOutIfTheSecondFileDoesnExist()
        {
            const string FILENAME = "somefile.test";

            try
            {
                File.Create(FILENAME).Close();

                Assert.IsFalse(ImageComparer.Compare(FILENAME, "none existing file"));
            }
            finally
            {
                if (File.Exists(FILENAME))
                {
                    File.Delete(FILENAME);
                }
            }
        }

        [Test]
        public void ProvidesEarlyOutForFilesWithDifferentSize()
        {
            const string FIRST_FILENAME = "testFile1.png";
            const string SECOND_FILENAME = "testFile2.png";

            try
            {
                using (var firstBitmap = new Bitmap(800, 600))
                {
                    firstBitmap.Save(FIRST_FILENAME, ImageFormat.Png);
                }
                using (var secondBitnap = new Bitmap(400, 300))
                {
                    secondBitnap.Save(SECOND_FILENAME, ImageFormat.Png);
                }

                Assert.IsFalse(ImageComparer.Compare(FIRST_FILENAME, SECOND_FILENAME));
            }
            finally
            {
                if (File.Exists(FIRST_FILENAME))
                {
                    File.Delete(FIRST_FILENAME);
                }
                if (File.Exists(SECOND_FILENAME))
                {
                    File.Delete(SECOND_FILENAME);
                }
            }
        }

        [Test]
        public void ProvidesEarlyOutForBitmapsWithDifferentSize()
        {
            using (var firstBitmap = new Bitmap(800, 600))
            {
                using (var secondBitmap = new Bitmap(400, 300))
                {
                    Assert.IsFalse(ImageComparer.Compare(firstBitmap, secondBitmap));
                }
            }
        }

        [Test]
        public void ReturnsTrueIfImagesMatchForBitmaps()
        {
            const string FILENAME = @"..\resources\tests\test.png";

            using (var firstBitmap = new Bitmap(FILENAME))
            {
                using (var secondBitmap = new Bitmap(FILENAME))
                {
                    Assert.IsTrue(ImageComparer.Compare(firstBitmap, secondBitmap));
                }
            }
        }

        [Test]
        public void ReturnsTrueIfImagesMatchForFiles()
        {
            const string FILENAME = @"..\resources\tests\test.png";

            Assert.IsTrue(ImageComparer.Compare(FILENAME, FILENAME));
        }
        
        [Test]
        public void ReturnsFalseIfOnePixelIsAltered()
        {
            const string FILENAME = @"..\resources\tests\test.png";

            using (var firstBitmap = new Bitmap(FILENAME))
            {
                using (var secondBitmap = new Bitmap(FILENAME))
                {
                    secondBitmap.SetPixel(0, 1, Color.Black);

                    Assert.IsFalse(ImageComparer.Compare(firstBitmap, secondBitmap));
                }
            }
        }
    }
}