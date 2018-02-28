using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Maker.Rise.Platform;

namespace AndroidPlatform
{
    public class RiseAndroidPlatform : IPlatform
    {
        private int _screenWidth;
        private int _screenHeight;
        public RiseAndroidPlatform(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public int GetHardwareHeight()
        {
            return _screenHeight;
        }

        public int GetHardwareWidth()
        {
            return _screenWidth;
        }

        public void Initialize()
        {

        }
    }
}