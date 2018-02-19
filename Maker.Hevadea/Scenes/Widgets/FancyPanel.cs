using Maker.Rise;
using Maker.Rise.Extension;
using Maker.Rise.Ressource;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Scenes.Widgets
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
            spriteBatch.Draw(Engine.Scene.BluredScene, Host, Host, Color.White);
            spriteBatch.FillRectangle(Host, Color);

            base.Draw(spriteBatch, gameTime);
            TopLeft.Draw(spriteBatch, new Rectangle(Bound.X, Bound.Y, 32, 32), Color.White);
            TopRight.Draw(spriteBatch, new Rectangle(Bound.Right - 32, Bound.Y, 32, 32), Color.White);
            BottomLeft.Draw(spriteBatch, new Rectangle(Bound.X, Bound.Bottom - 32, 32, 32), Color.White);
            BottomRight.Draw(spriteBatch, new Rectangle(Bound.Right - 32, Bound.Bottom - 32, 32, 32), Color.White);

            Top.Draw(spriteBatch, new Rectangle(Bound.X + 32, Bound.Y, Bound.Width - 64, 32), Color.White);
            Bottom.Draw(spriteBatch, new Rectangle(Bound.X + 32, Bound.Y + Bound.Height - 32, Bound.Width - 64, 32), Color.White);

            Left.Draw(spriteBatch, new Rectangle(Bound.X, Bound.Y + 32, 32, Bound.Height - 64), Color.White);
            Right.Draw(spriteBatch, new Rectangle(Bound.X + Bound.Width - 32, Bound.Y + 32, 32, Bound.Height - 64), Color.White);
        }
    }
}
