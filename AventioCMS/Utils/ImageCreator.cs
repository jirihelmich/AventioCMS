using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace HTH8.Utils
{
    public class ImageCreator
    {
        public static Image CreateCarouselImage(string sImageText, string number)
        {

            Bitmap objBmpImage = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            int fontSize = 11;
            int numFontSize = 14;

            // Create the Font object for the image text drawing.
            Font objFont = new Font("Tahoma", fontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            Font objNumFont = new Font("Tahoma", numFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            // This is where the bitmap size is determined.
            intWidth = 35;
            intHeight = 210;

            int numWidth = (int)objGraphics.MeasureString(number.ToString(), objNumFont).Width;
            int numHeight = (int)objGraphics.MeasureString(number.ToString(), objNumFont).Height;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);
            objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

            objGraphics.TranslateTransform((intWidth / 2) - numWidth / 2, intHeight - numHeight - 2);

            // Set Background color
            objGraphics.Clear(Color.FromArgb(238, 238, 238));
            objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            objGraphics.DrawString(number.ToString(), objNumFont, new SolidBrush(Color.FromArgb(130, 130, 130)), 0, 0);

            objGraphics.ResetTransform();

            try
            {
                int fontWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width;
                int fontHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height;

                objGraphics.TranslateTransform((intWidth / 2) - fontHeight / 2, intHeight - 20);
                objGraphics.RotateTransform(270);

                // Set Background color
                objGraphics.DrawString(sImageText, objFont, new SolidBrush(Color.FromArgb(130, 130, 130)), 0, 0);
            }
            catch (Exception e)
            {

            }
            objGraphics.Flush();

            return (objBmpImage);

        }

    }
}
