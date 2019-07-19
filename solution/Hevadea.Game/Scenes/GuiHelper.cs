using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Scenes
{
    public static class GuiHelper
    {
        private static bool _loaded = false;

        private static Sprite TopLeft;
        private static Sprite TopRight;
        private static Sprite BottomLeft;
        private static Sprite BottomRight;
        private static Sprite Top;
        private static Sprite Bottom;
        private static Sprite Left;
        private static Sprite Right;

        private static void LoadSprites()
        {
            if (_loaded) return;

            TopLeft = new Sprite(Resources.TileGui, new Point(0, 0));
            TopRight = new Sprite(Resources.TileGui, new Point(1, 0));
            BottomLeft = new Sprite(Resources.TileGui, new Point(0, 1));
            BottomRight = new Sprite(Resources.TileGui, new Point(1, 1));

            Top = new Sprite(Resources.TileGui, new Point(2, 0));
            Bottom = new Sprite(Resources.TileGui, new Point(3, 1));
            Left = new Sprite(Resources.TileGui, new Point(2, 1));
            Right = new Sprite(Resources.TileGui, new Point(3, 0));
        }

        public static void DrawBox(this SpriteBatch sb, Rectangle Bound, int size)
        {
            LoadSprites();

            TopLeft.Draw(sb, new Rectangle(Bound.X, Bound.Y, size, size), Color.White);
            TopRight.Draw(sb, new Rectangle(Bound.Right - size, Bound.Y, size, size), Color.White);
            BottomLeft.Draw(sb, new Rectangle(Bound.X, Bound.Bottom - size, size, size), Color.White);
            BottomRight.Draw(sb, new Rectangle(Bound.Right - size, Bound.Bottom - size, size, size), Color.White);

            Top.Draw(sb, new Rectangle(Bound.X + size, Bound.Y, Bound.Width - size * 2, size), Color.White);
            Bottom.Draw(sb, new Rectangle(Bound.X + size, Bound.Y + Bound.Height - size, Bound.Width - size * 2, size),
                Color.White);

            Left.Draw(sb, new Rectangle(Bound.X, Bound.Y + size, size, Bound.Height - size * 2), Color.White);
            Right.Draw(sb, new Rectangle(Bound.X + Bound.Width - size, Bound.Y + size, size, Bound.Height - size * 2),
                Color.White);
        }
    }
}