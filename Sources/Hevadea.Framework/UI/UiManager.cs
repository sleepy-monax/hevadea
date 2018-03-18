using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
    public class UiManager
    {
        public SpriteFont DefaultFont { get; set; }
        public SpriteFont DebugFont { get; set; }
        public float ScaleFactor { get; set; } = 1.5f;
        public Widget FocusWidget { get; set; }
    }
}