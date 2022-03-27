using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OcrRoid.Ocr;
using OcrRoid.Talks;
using OcrRoid.Util;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Point = System.Windows.Point;

namespace OcrRoid.View
{
    /// <summary>
    /// ClippingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ClippingWindow : Window
    {
        public ClippingWindow()
        {
            InitializeComponent();
            this.Cursor = Cursors.Cross;

            var mouseLeftDown = Observable.FromEventPattern<MouseEventArgs>(this, "MouseLeftButtonDown");
            var mouseMove = Observable.FromEventPattern<MouseEventArgs>(this, "MouseMove");
            var mouseUp = Observable.FromEventPattern<MouseEventArgs>(this, "MouseLeftButtonUp");
      


            var origin = new Point();
            mouseLeftDown
                .Do(e => { origin = e.EventArgs.GetPosition(Root); })
                .SelectMany(mouseMove)
                .TakeUntil(mouseUp)
                .Do(e =>
                {

                    var rect = BoundsRect(origin.X, origin.Y, e.EventArgs.GetPosition(Root).X, e.EventArgs.GetPosition(Root).Y);
                    ClippingRect.Margin = new Thickness(rect.Left, rect.Top, this.Width - rect.Right, this.Height - rect.Bottom);
                    ClippingRect.Width = rect.Width;
                    ClippingRect.Height = rect.Height;
                })
                .LastAsync()
                .Subscribe(async e =>
                {
                    try
                    {
                        this.Hide();
                        await this.OcrAsync(origin, e.EventArgs);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        Cursor = Cursors.Arrow;
                        this.Close();
                    }
                });

        }

        private async Task OcrAsync(Point origin, MouseEventArgs e)
        {

            var bmp = CaptureScreen(Rect.Offset(BoundsRect(origin.X, origin.Y, e.GetPosition(Root).X, e.GetPosition(Root).Y), this.Left, this.Top));

            var ocr = new WindowsOcr();
            var text = await ocr.OcrFromImageAsync(bmp);
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("文字が読み取れませんでした", "認識エラー");
                return;
            }
            
            var talk = TalkFactory.FactTalkFromSettings();
            await talk.SpeakAsync(text);
        }

        private static Rect BoundsRect(double left, double top, double right, double bottom)
        {
            return new Rect(Math.Min(left, right), Math.Min(top, bottom), Math.Abs(right - left), Math.Abs(bottom - top));
        }



        public static Bitmap CaptureScreen(Rect rect)
        {
            var bmp = new Bitmap((int)rect.Width, (int)rect.Height, PixelFormat.Format32bppArgb);

            using var graphics = Graphics.FromImage(bmp);

            graphics.CopyFromScreen((int)rect.X, (int)rect.Y, 0, 0, bmp.Size);

            return bmp;

        }

        private void OnKeyDownCloseWindow(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
