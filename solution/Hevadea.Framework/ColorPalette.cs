using Microsoft.Xna.Framework;

namespace Hevadea.Framework
{
    public static class ColorPalette
    {
        public static Color Accent => new Color(254, 174, 52);
        public static Color BorderIdle => new Color(90, 105, 136) * 0.75f;
        public static Color BorderOver => ColorPalette.Accent;
        public static Color BorderDown => new Color(139, 155, 180);
        public static Color BackgroundIdle => new Color(90, 105, 136) * 0.5f;
        public static Color BackgroundOver => ColorPalette.Accent * 0.75f;
        public static Color BackgroundDown => new Color(139, 155, 180) * 0.75f;
    }
}