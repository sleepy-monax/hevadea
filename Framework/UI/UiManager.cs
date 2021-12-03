using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
    public class UiManager
    {
        public bool Enabled { get; set; } = true;
        public SpriteFont DefaultFont { get; set; }
        public SpriteFont DebugFont { get; set; }
        public float ScaleFactor => Rise.Config.UIScaling * Rise.Platform.GetScreenScaling();
        public Widget FocusWidget { get; set; }

        public void RefreshLayout()
        {
            Rise.Scene.GetCurrentScene()?.RefreshLayout();
        }
    }
}