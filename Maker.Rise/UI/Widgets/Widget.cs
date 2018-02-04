using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Rise.UI.Widgets
{
    public class Padding
    {
        public int Up { get; set; }
        public int Down { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }

        public Padding(int up, int down, int left, int right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }

        public Padding(int all)
        {
            Up = Down = Left = Right = all;
        }
    }

    public class Widget
    {
        public Rectangle Bound { get; set; } = new Rectangle(0, 0, 64, 64);
        public Rectangle Host { get { return new Rectangle(Bound.X + Padding.Left, Bound.Y + Padding.Up, Bound.Width - Padding.Left - Padding.Right, Bound.Height - Padding.Up - Padding.Down); } }

        public Padding Padding { get; set; }
        public List<Widget> Childrens { get; set; } = new List<Widget>();

        public delegate void WidgetEventHandler(Widget sender);
        public event WidgetEventHandler MouseClick;

        public void HandleInput()
        {

        }

        public void UpdateInternal(GameTime gameTime)
        {
            if (Bound.Contains(Engine.Input.MousePosition) && Engine.Input.MouseLeftClick)
            {
                MouseClick?.Invoke(this);
            }

            Update(gameTime);
        }

        public virtual void Update(GameTime gameTime)
        {

        }


        public void DrawIternal(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Draw(spriteBatch, gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
    }
}
