﻿using System;
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
using WPFLayer.ServicesImplementation;
using Validation;

namespace WPFLayer.Assets
{
    public static class ImageHelper
    {

        public static BitmapSource GetBitmapImageFor(GamesPlayers gamePlayer)
        {
            Bitmap avatar = null;
            if (gamePlayer.Player.User.idUserType != (int)UserTypes.GUEST)
            {
                avatar =
                    (Bitmap)ResourcesProfileImages.ResourceManager.GetObject(gamePlayer.Player.ProfileImage.profileImageName);
            }
            else
            {
                avatar = (Bitmap)ResourcesProfileImages.ResourceManager.GetObject("Lemon");
            }


            return Imaging.CreateBitmapSourceFromHBitmap
                    (
                        avatar.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions()
                    );
        }
    }
}
