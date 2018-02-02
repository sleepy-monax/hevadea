using System;
using Maker.Rise.Extension;
using Maker.Rise.UI.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public class BlurPanel : Panel
    {
        public Color BackgroundColor { get; set; } = Color.Black * 0.8f;
        public Color BorderColor { get; set; } = Color.White * 0.1f;
        
        public BlurPanel(IContainer container) : base(container){}
        
        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Engine.Scene.BlurRT, Bound, Bound, Color.White);
            spriteBatch.FillRectangle(Bound, BackgroundColor);
            spriteBatch.DrawRectangle(Bound, BorderColor);
            
            base.OnDraw(spriteBatch, gameTime);
        }
    }
}