namespace WorldOfImagination.Utils
{
    public static class Screen
    {

        public static int GetWidth()
        {
            return System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        }

        public static int GetHeight()
        {
            return System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        }

    }
}
