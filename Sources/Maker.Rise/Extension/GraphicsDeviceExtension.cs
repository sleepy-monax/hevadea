namespace Maker.Rise.Extension
{
    public static class GraphicsDeviceManager
    {
        public static int GetWidth(this Microsoft.Xna.Framework.GraphicsDeviceManager graphic)
        {
            return graphic.PreferredBackBufferWidth;
        }

        public static int GetHeight(this Microsoft.Xna.Framework.GraphicsDeviceManager graphic)
        {
            return graphic.PreferredBackBufferHeight;
        }

        public static void SetWidth(this Microsoft.Xna.Framework.GraphicsDeviceManager graphic, int width)
        {
            graphic.PreferredBackBufferWidth = width;
        }

        public static void SetHeight(this Microsoft.Xna.Framework.GraphicsDeviceManager graphic, int height)
        {
            graphic.PreferredBackBufferHeight = height;
        }

        public static void Apply(this Microsoft.Xna.Framework.GraphicsDeviceManager graphic)
        {
            graphic.ApplyChanges();
        }

        public static void SetFullScreen(this Microsoft.Xna.Framework.GraphicsDeviceManager graphic)
        {
            graphic.SetWidth(Screen.GetWidth());
            graphic.SetHeight(Screen.GetHeight());
            graphic.Apply();


            graphic.ToggleFullScreen();
        }
    }
}