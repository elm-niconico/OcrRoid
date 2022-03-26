using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using OcrRoid.Util;
using BitmapDecoder = Windows.Graphics.Imaging.BitmapDecoder;


namespace OcrRoid.Ocr
{
    internal class WindowsOcr
    {

        public async Task<string> OcrFromImageAsync(Bitmap bmp)
        {
            
            var ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();
            var withMargin = BitmapUti.GiveMargin(bmp, 200);
            var result =  await ocrEngine.RecognizeAsync(await BitmapUti.ConvertToSoftwareBitmapAsync(withMargin));
            return Regex.Replace(result.Text, @"\s", "");
        }

    
    }
}
