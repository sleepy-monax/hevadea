using Maker.Rise.Enums;
using Maker.Rise.Extension;
using Maker.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Maker.Rise.UI.Containers;
using Microsoft.Xna.Framework.Input;
using MouseState = Maker.Rise.Enums.MouseState;

namespace Maker.Rise.UI
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

    public abstract class Control
    {
        public bool Visible      { get; set; } = true;
        
        public Point Location { get; set; } = new Point(0,0);
        public Point Size { get; set; } = new Point(64, 64);
        public Padding Padding   { get; set; } = new Padding(0);
        
        public Rectangle Bound   {
            get
            {
                return _onScreenRectangle ?? new Rectangle(Location, Size);
            }
            set 
            {
                Location = value.Location;
                Size = value.Size;
            }
        }
        
        public LayoutMode Layout { get; set; } = LayoutMode.Dock;
        public Dock Dock         { get; set; } = Dock.None;
        public Anchor Origine { get; set; } = Anchor.TopLeft;
        public Anchor Anchor { get; set; } = Anchor.TopLeft;
        
        public Rectangle Host => new Rectangle(Bound.X + Padding.Left, Bound.Y + Padding.Up, Bound.Width - Padding.Left - Padding.Right, Bound.Height - Padding.Up - Padding.Down);

        private MouseState _mouseState = MouseState.None;
        private Rectangle? _onScreenRectangle = null;

        public event OnMouseClickHandler MouseClick;
        public delegate void OnMouseClickHandler(Control sender, MouseEventArgs e);
        public virtual void OnMouseClick(){}

        public event OnMouseEnterHandler MouseEnter;
        public delegate void OnMouseEnterHandler(Control sender, MouseEventArgs e);
        public virtual void OnMouseEnter(){}
        
        public event OnMouseExitHandler MouseExit;
        public delegate void OnMouseExitHandler(Control sender, MouseEventArgs e);
        public virtual void OnMouseExit(){}
        
        public event OnMouseMoveHandler MouseMove;
        public delegate void OnMouseMoveHandler(Control sender, MouseEventArgs e);
        public virtual void OnMouseMove(){}

        public event OnMouseDownHandler MouseDown;
        public delegate void OnMouseDownHandler(Control sender, MouseEventArgs e);
        public virtual void OnMouseDown(){}

        public event OnMouseUpHandler MouseUp;
        public delegate void OnMouseUpHandler(Control sender, MouseEventArgs e);
        public virtual void OnMouseUp(){}

        public virtual void RefreshLayout()
        {
        }

        public void ApplyLayout(Rectangle onScreenRectangle)
        {
            _onScreenRectangle = onScreenRectangle;
        }

        public MouseState GetMouseState()
        {
            return _mouseState;
        }

        protected abstract void OnDraw(SpriteBatch spriteBatch, GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            OnDraw(spriteBatch, gameTime);
            
            if (Engine.Ui.Debug)
            {
                spriteBatch.DrawRectangle(Bound, Color.Black);
            }
        }


        protected abstract void OnUpdate(GameTime gameTime);

        public void Update(GameTime gameTime)
        {

            var mouseState = Engine.Input.GetMouseState();
            var oldMouseState = Engine.Input.GetOldMouseState();
            var mouseOver = Bound.Contains(mouseState.Position);

            var isMouseDown = mouseState.LeftButton == ButtonState.Pressed && mouseOver;
            var wasMouseDown = oldMouseState.LeftButton == ButtonState.Pressed && mouseOver;
            
            _mouseState = mouseOver ? (isMouseDown ? MouseState.Down : MouseState.Over) : MouseState.None;
            
            if (!Bound.Contains(oldMouseState.Position) && mouseOver)
            {
                // The mouse enter the control bound.
                MouseEnter?.Invoke(this, new MouseEventArgs(mouseState));
                OnMouseEnter();
            }
            
            if (Bound.Contains(oldMouseState.Position) && !mouseOver)
            {
                // The mouse exit the control bound.
                MouseExit?.Invoke(this, new MouseEventArgs(mouseState));
                OnMouseExit();
            }

            if (!wasMouseDown && isMouseDown)
            {
                MouseDown?.Invoke(this, new MouseEventArgs(mouseState));
                OnMouseDown();
            }
            
            if (wasMouseDown && !isMouseDown)
            {
                MouseUp?.Invoke(this, new MouseEventArgs(mouseState));
                OnMouseUp();
            }

            if (mouseOver && oldMouseState.Position != mouseState.Position)
            {
                MouseMove?.Invoke(this, new MouseEventArgs(mouseState));
                OnMouseMove();
            }

            if (mouseOver && Engine.Input.MouseLeftClick)
            {
                MouseClick?.Invoke(this, new MouseEventArgs(mouseState));
                OnMouseClick();
            }

            OnUpdate(gameTime);
        }
    }
}