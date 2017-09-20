using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BarSteward.Services
{
    static class Encode
    {
        public static string Base64Encode(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                byte[] imageBytes = stream.ToArray();
                return Convert.ToBase64String(imageBytes);                
            }
        }
    }
}
