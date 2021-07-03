using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Media.Effects;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace Spellmaker
{
    public class Grapich
    {
        public static System.Drawing.Bitmap Render(string inputFile, FormattedText text, System.Windows.Point position)
        {
            var ret =  makeBitmap(inputFile, text, position);
 
            
            return GetBitmap(ret);
        }
        public static void Do()
        {
            const string testString = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor";
            //const string fileName = "test.png";
            const int scale = 1;
            // Create the initial formatted text string.
            FormattedText text = new FormattedText(
                testString,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                32,
                System.Windows.Media.Brushes.Black)
            {
                MaxTextWidth = 300,
                MaxTextHeight = 240
            };
            text.SetFontSize(36 * (96.0 / 72.0), 0, 5);
            text.SetFontWeight(FontWeights.Bold, 6, 11);

        }
        public static void WriteTextToImage(string inputFile, string outputFile, FormattedText text, System.Windows.Point position)
        {
            RenderTargetBitmap target = makeBitmap(inputFile, text, position);

            BitmapEncoder encoder = null;

            switch (Path.GetExtension(outputFile))
            {
                case ".png":
                    encoder = new PngBitmapEncoder();
                    break;
                    // more encoders here
            }

            if (encoder != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(target));
                using (FileStream outputStream = new FileStream(outputFile, FileMode.Create))
                {
                    encoder.Save(outputStream);
                }
            }
        }

        private static RenderTargetBitmap makeBitmap(string inputFile, FormattedText text, System.Windows.Point position)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(Path.GetFullPath(inputFile))); // inputFile must be absolute path
            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawImage(bitmap, new Rect(0, 0, bitmap.Width, bitmap.Height));
                dc.DrawText(text, position);
            }
            RenderTargetBitmap target = new RenderTargetBitmap(bitmap.PixelWidth, bitmap.PixelHeight,
                                                               bitmap.DpiX, bitmap.DpiY, PixelFormats.Default);
            target.Render(visual);
            return target;
        }
        public static System.Drawing.Bitmap GetBitmap(BitmapSource source)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
              source.PixelWidth,
              source.PixelHeight,
              System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            BitmapData data = bmp.LockBits(
              new Rectangle(System.Drawing.Point.Empty, bmp.Size),
              ImageLockMode.WriteOnly,
              System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            source.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }
    }
}



