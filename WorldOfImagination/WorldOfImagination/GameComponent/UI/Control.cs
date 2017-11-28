using System;
﻿using Microsoft.Xna.Framework;

namespace WorldOfImagination.GameComponent.UI
{
    public enum Anchor
    {
        UpLeft, Up, UpRight,
        Left, center, Right,
        DownLeft, Down, DownRight
    }

    public enum Dock
    {
        Top, Right, Bottom, Left, Fill, None
    }

    public abstract class Control
    {
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        private UIManager UI;

        public Control(UIManager ui)
        {
            UI = ui;
        }

        public abstract void OnLayout();
        public void Layout()
        {
            OnLayout();
        }

        public abstract void OnDraw(GameTime gameTime);
        public void Draw(GameTime gameTime)
        {
            OnDraw(gameTime);
        }

        public abstract void OnUpdate(GameTime gameTime);
        public void Update(GameTime gameTime)
        {
            OnUpdate(gameTime);
        }
    }
}
