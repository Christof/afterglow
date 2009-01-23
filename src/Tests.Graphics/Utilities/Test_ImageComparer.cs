using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MbUnit.Framework;

namespace TheNewEngine.Graphics.Utilities
{
    public class Test_ImageComparer
    {
        private const string TEST_FILENAME = "test.png";

        [Test]
        public void Compare_provides_early_out_if_first_file_does_not_exist()
        {
            Assert.IsFalse(ImageComparer.Compare("none existing file", "doesnt matter"));
        }

        [Test]
        public void Compare_provides_early_out_if_second_file_does_not_exist()
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
        public void Compare_provides_early_out_for_files_with_different_size()
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
        public void Compare_ürovides_early_out_for_bitmaps_with_different_size()
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
        public void Compare_returns_true_if_images_match_for_bitmaps()
        {
            using (var firstBitmap = new Bitmap(TEST_FILENAME))
            {
                using (var secondBitmap = new Bitmap(TEST_FILENAME))
                {
                    Assert.IsTrue(ImageComparer.Compare(firstBitmap, secondBitmap));
                }
            }
        }

        [Test]
        public void Compare_returns_true_for_same_image()
        {
            Assert.IsTrue(ImageComparer.Compare(TEST_FILENAME, TEST_FILENAME));
        }
        
        [Test]
        public void Compare_returns_false_if_one_pixel_was_altered()
        {
            using (var firstBitmap = new Bitmap(TEST_FILENAME))
            {
                using (var secondBitmap = new Bitmap(TEST_FILENAME))
                {
                    secondBitmap.SetPixel(0, 1, Color.Black);

                    Assert.IsFalse(ImageComparer.Compare(firstBitmap, secondBitmap));
                }
            }
        }

        [Test]
        public void ComparePerByte_returns_true_for_same_image()
        {
            using (var firstBitmap = new Bitmap(TEST_FILENAME))
            {
                using (var secondBitmap = new Bitmap(TEST_FILENAME))
                {
                    Assert.IsTrue(ImageComparer.ComparePerByte(firstBitmap, secondBitmap));
                }
            }
        }

        [Test]
        public void ComparePerByte_returns_false_if_one_pixel_was_altered()
        {
            using (var firstBitmap = new Bitmap(TEST_FILENAME))
            {
                using (var secondBitmap = new Bitmap(TEST_FILENAME))
                {
                    secondBitmap.SetPixel(0, 1, Color.Black);

                    Assert.IsFalse(ImageComparer.ComparePerByte(firstBitmap, secondBitmap));
                }
            }
        }

        [Test]
        public void ComparePerBytel_provides_early_out_for_bitmaps_with_different_size()
        {
            using (var firstBitmap = new Bitmap(800, 600))
            {
                using (var secondBitmap = new Bitmap(400, 300))
                {
                    Assert.IsFalse(ImageComparer.ComparePerByte(firstBitmap, secondBitmap));
                }
            }
        }

        [Test]
        public void ComparePerPixel_returns_true_for_same_image()
        {
            using (var firstBitmap = new Bitmap(TEST_FILENAME))
            {
                using (var secondBitmap = new Bitmap(TEST_FILENAME))
                {
                    Assert.IsTrue(ImageComparer.ComparePerPixel(firstBitmap, secondBitmap));
                }
            }
        }

        [Test]
        public void ComparePerPixel_returns_false_if_one_pixel_was_altered()
        {
            using (var firstBitmap = new Bitmap(TEST_FILENAME))
            {
                using (var secondBitmap = new Bitmap(TEST_FILENAME))
                {
                    secondBitmap.SetPixel(0, 1, Color.Black);

                    Assert.IsFalse(ImageComparer.ComparePerPixel(firstBitmap, secondBitmap));
                }
            }
        }

        [Test]
        public void ComparePerPixel_provides_early_out_for_bitmaps_with_different_size()
        {
            using (var firstBitmap = new Bitmap(800, 600))
            {
                using (var secondBitmap = new Bitmap(400, 300))
                {
                    Assert.IsFalse(ImageComparer.ComparePerPixel(firstBitmap, secondBitmap));
                }
            }
        }
    }
}