using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;

namespace OcrRoid.Util
{
    internal class BitmapUti
    {
        private BitmapUti(){}

        public static Bitmap GiveMargin(Bitmap originBmp, int margin)
        {
            using (originBmp)
            {
                var width = originBmp.Width + 2 * margin;
                var height = originBmp.Height + 2 * margin;
                var newBmp = new Bitmap(width, height);
                using var graphics = Graphics.FromImage(newBmp);

                var brush = new SolidBrush(originBmp.GetPixel(0, 0));
                graphics.FillRectangle(brush, 0, 0, width, height);
                graphics.DrawImage(originBmp, new Rectangle(margin, margin, originBmp.Width, originBmp.Height));

                return newBmp;
            }
        }


        public static async Task<SoftwareBitmap> ConvertToSoftwareBitmapAsync(Bitmap bmp)
        {
            const string fileName = "dummy.png";
            bmp.Save($@"{Directory.GetCurrentDirectory()}/{fileName}", ImageFormat.Png);

            var appFolder = await StorageFolder.GetFolderFromPathAsync(Directory.GetCurrentDirectory());
            var file = await appFolder.GetFileAsync(fileName);

            using var stream = await file.OpenAsync(FileAccessMode.Read);

            var decoder = await BitmapDecoder.CreateAsync(stream);

            return await decoder.GetSoftwareBitmapAsync();
        }
    }
}
