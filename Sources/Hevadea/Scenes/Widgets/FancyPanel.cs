using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Scenes.Widgets
{
    public class FancyPanel : Panel
    {
        public Color Color { get; set; } = Color.Black * 0.9f;

        Sprite TopLeft;
        Sprite TopRight;
        Sprite BottomLeft;
        Sprite BottomRight;

        Sprite Top;
        Sprite Bottom;
        Sprite Left;
        Sprite Right;

        public FancyPanel()
        {
            TopLeft = new Sprite(Ressources.TileGui, new Point(0, 0));
            TopRight = new Sprite(Ressources.TileGui, new Point(1, 0));
            BottomLeft = new Sprite(Ressources.TileGui, new Point(0, 1));
            BottomRight = new Sprite(Ressources.TileGui, new Point(1, 1));

            Top = new Sprite(Ressources.TileGui, new Point(2, 0));
            Bottom = new Sprite(Ressources.TileGui, new Point(3, 1));
            Left = new Sprite(Ressources.TileGui, new Point(2, 1));
            Right = new Sprite(Ressources.TileGui, new Point(3, 0));
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Host, Color);

            base.Draw(spriteBatch, gameTime);

            var size = Scale(32);
            TopLeft.Draw(spriteBatch, new Rectangle(Bound.X, Bound.Y, size, size), Color.White);
            TopRight.Draw(spriteBatch, new Rectangle(Bound.Right - size, Bound.Y, size, size), Color.White);
            BottomLeft.Draw(spriteBatch, new Rectangle(Bound.X, Bound.Bottom - size, size, size), Color.White);
            BottomRight.Draw(spriteBatch, new Rectangle(Bound.Right - size, Bound.Bottom - size, size, size), Color.White);

            Top.Draw(spriteBatch, new Rectangle(Bound.X + size, Bound.Y, Bound.Width - size * 2, size), Color.White);
            Bottom.Draw(spriteBatch, new Rectangle(Bound.X + size, Bound.Y + Bound.Height - size, Bound.Width - size * 2, size), Color.White);

            Left.Draw(spriteBatch, new Rectangle(Bound.X, Bound.Y + size, size, Bound.Height - size * 2), Color.White);
            Right.Draw(spriteBatch, new Rectangle(Bound.X + Bound.Width - size, Bound.Y + size, size, Bound.Height - size * 2), Color.White);
        }
    }
}
