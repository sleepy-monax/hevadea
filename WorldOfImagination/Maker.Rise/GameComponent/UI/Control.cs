using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Maker.Rise.GameComponent.UI
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
        public Point MaximuSize { get; set; } = Point.Zero;
        private Rectangle Host => new Rectangle(Bound.X + Padding.Left,
            Bound.Y + Padding.Up,
            Bound.Width - Padding.Left - Padding.Right,
            Bound.Height - Padding.Up - Padding.Down);

        public bool Enabled { get; set; } = true;
        public bool Visible { get; set; } = true;

        public MouseState MouseState = MouseState.None;
        public MouseState OldMouseState = MouseState.None;

        public Padding Padding { get; set; } = new Padding(0);
        public Anchor Anchor { get; set; } = Anchor.Center;
        public Dock Dock { get; set; } = Dock.None;
        public LayoutMode Layout { get; set; } = LayoutMode.Dock;

        public readonly List<Control> Childs;

        public event OnMouseClickHandler OnMouseClick;
        public delegate void OnMouseClickHandler(object sender, EventArgs e);

        private bool NoManagedMouseClick = false;
        public Control(UiManager ui)
        {
            UI = ui;
            Childs = new List<Control>();
        }
        
        public Control(UiManager ui, bool noManagedMouseClick)
        {
            UI = ui;
            Childs = new List<Control>();
            NoManagedMouseClick = noManagedMouseClick;
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
                var hostWidth = host.Width;
                var hostHeight = host.Height;
                var childsCount = Childs.Count;
                foreach (var c in Childs)
                {
                    var width = hostWidth / childsCount;
                    var height = hostHeight / childsCount;

                    if (c.MaximuSize != Point.Zero)
                    {
                        if (width > c.MaximuSize.X)
                        {
                            width = c.MaximuSize.X;
                        }
                        
                        if (height > c.MaximuSize.Y)
                        {
                            height = c.MaximuSize.Y;
                        }
                        
                        //TODO: Finish handeling controls maximum size on layout.
                    }

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
            //UI.Game.GraphicsDevice.ScissorRectangle = Bound;
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

        public void RaiseOnMouseClick()
        {
            OnMouseClick?.Invoke(this, EventArgs.Empty);
        }

        protected abstract void OnUpdate(GameTime gameTime);
        public void Update(GameTime gameTime)
        {
            OldMouseState = MouseState;
            if (Bound.Contains(UI.Input.MousePosition))
            {
                MouseState = MouseState.Over;

                if (UI.Input.MouseLeft)
                {
                    MouseState = MouseState.Down;
                }

                if (UI.Input.MouseLeftClick && !NoManagedMouseClick)
                {
                    RaiseOnMouseClick();
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
