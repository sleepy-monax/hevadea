using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Utils;

namespace WorldOfImagination.GameComponent.UI
{
    public enum Anchor
    {
        UpLeft, Up, UpRight,
        Left, Center, Right,
        DownLeft, Down, DownRight
    }

    public enum Dock
    {
        Top, Right, Bottom, Left, Fill, None
    }

    public enum LayoutMode
    {
        Dock, Vertical, Horizontal
    }

    public enum MouseState
    {
        None, Over, Down
    }
    
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

    public abstract class Control
    {
        public UiManager UI;

        public Rectangle Bound { get; set; } = Rectangle.Empty;
        private Rectangle Host {
            get
            {
                return new Rectangle(Bound.X + Padding.Left,
                    Bound.Y + Padding.Up,
                    Bound.Width - Padding.Left - Padding.Right,
                    Bound.Height - Padding.Up - Padding.Down);
            }
        }

        public bool Enabled { get; set; } = true;
        public bool Visible { get; set; } = true;

        public MouseState MouseState = MouseState.None;
        
        public Padding Padding { get; set; } = new Padding(0);
        public Anchor Anchor { get; set; } = Anchor.Center;
        public Dock Dock { get; set; } = Dock.None;
        public LayoutMode Layout { get; set; } = LayoutMode.Dock;

        public readonly List<Control> Childs;

        public event OnMouseClickHandler OnMouseClick;
        public delegate void OnMouseClickHandler(object sender, EventArgs e);
        
        public Control(UiManager ui)
        {
            UI = ui;
            Childs = new List<Control>();
        }

        public void AddChild(Control child)
        {
            Childs.Add(child);
        }
        
        public void RemoveChild(Control child)
        {
            Childs.Remove(child);
        }

        public void RefreshLayout()
        {

            Rectangle host = Host;

            if (Layout == LayoutMode.Dock)
            {
            
                foreach (var c in Childs)
                {
                    
                    switch (c.Dock)
                    {
                        case Dock.Top:
                            c.Bound = new Rectangle(host.X, host.Y,
                                                    host.Width, c.Bound.Height);
                            
                            host = new Rectangle(host.X, host.Y + c.Bound.Height,
                                                 host.Width, host.Height - c.Bound.Height);
                            break;
                        case Dock.Right:
                            c.Bound = new Rectangle(host.X + host.Width - c.Bound.Width, host.Y,
                                c.Bound.Width, host.Height);
                            
                            host = new Rectangle(host.X, host.Y,
                                host.Width - c.Bound.Width, host.Height);
                            
                            break;
                        case Dock.Bottom:
                            c.Bound = new Rectangle(host.X, host.Y + host.Height - c.Bound.Height,
                                                    host.Width, c.Bound.Height);
                            
                            host = new Rectangle(host.X, host.Y,
                                                 host.Width, host.Height - c.Bound.Height);
                            break;
                        case Dock.Left:
                            c.Bound = new Rectangle(host.X, host.Y,
                                c.Bound.Width, host.Height);
                            
                            host = new Rectangle(host.X + c.Bound.Width, host.Y,
                                host.Width - c.Bound.Width, host.Height);
                            break;
                        case Dock.Fill:
                            c.Bound = host;
                            break;
                        case Dock.None:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    c.RefreshLayout();
                }
            }
            else if (Layout == LayoutMode.Horizontal || Layout == LayoutMode.Vertical)
            {
                var i = 0;
                foreach (var c in Childs)
                {
                    var width = host.Width / Childs.Count;
                    var height = host.Height / Childs.Count;
                    
                    c.Bound = Layout == LayoutMode.Horizontal ? new Rectangle(host.X + width * i,  host.Y, width ,host.Height) 
                                                              : new Rectangle(host.X, host.Y + height * i, host.Width, height);

                    i++;
                    c.RefreshLayout();
                }
            }
        }

        protected abstract void OnDraw(SpriteBatch spriteBatch,GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            OnDraw(spriteBatch, gameTime);
            foreach (var c in Childs)
            {
                c.Draw(spriteBatch, gameTime);
            }

            if (UI.Debug)
            {
                spriteBatch.DrawRectangle(Host, Color.Red);
                spriteBatch.DrawRectangle(Bound, Color.Black);
            }
        }

        protected abstract void OnUpdate(GameTime gameTime);
        public void Update(GameTime gameTime)
        {
            if (Bound.Contains(UI.Input.MousePosition))
            {
                MouseState = MouseState.Over;

                if (UI.Input.MouseLeft)
                {
                    MouseState = MouseState.Down;
                }

                if (UI.Input.MouseLeftClick)
                {
                    OnMouseClick?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                MouseState = MouseState.None;
            }

            OnUpdate(gameTime);
            foreach (var c in Childs)
            {
                c.Update(gameTime);
            }
        }
    }
}
