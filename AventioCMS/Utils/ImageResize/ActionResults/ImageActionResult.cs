using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace HTH8.Utils.ImageResize.ActionResults
{
    /// <summary>
    /// To save and retrieve image in different sizes.
    /// </summary>
    public class ImageActionResult : ActionResult
    {

        private Image _SourceImage;

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <param name="SourceImage">resized image details</param>
        public ImageActionResult(Image SourceImage)
        {
            this._SourceImage = SourceImage;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            this.GenerateImage(context);
        }

        /// <summary>
        /// save different size of image
        /// </summary>
        /// <param name="context"></param>
        private void GenerateImage(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "image/png";

            MemoryStream ms = new MemoryStream();

            this._SourceImage.Save(ms, ImageFormat.Png);
            this._SourceImage.Dispose();

            ms.WriteTo(context.HttpContext.Response.OutputStream);
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
