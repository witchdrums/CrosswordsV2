using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;
using WPFLayer.Properties;

namespace WPFLayer.Assets
{
    public class ImageHelper
    {

        public static BitmapSource GetBitmapImageFor(String profileImageName)
        {
            Bitmap bitmapProfileImage =
                (Bitmap)ResourcesProfileImages.ResourceManager.GetObject(profileImageName);
            return Imaging.CreateBitmapSourceFromHBitmap
                    (
                        bitmapProfileImage.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions()
                    );
        }
    }
}
