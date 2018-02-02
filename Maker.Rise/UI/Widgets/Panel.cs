using System.Collections.Generic;
using Maker.Rise.Extension;
using Maker.Rise.UI.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public class Panel : Control
    {
        public IContainer Container { get; }
        
        public List<Control> Childs
        {
            get => Container.Childs;
            set => Container.Childs = value;
        }

        public Panel(IContainer container)
        {
            Container = container;
        }

        public override void RefreshLayout()
        {
            Container.RefreshLayout(this.Host);
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Engine.Debug.Visible) spriteBatch.FillRectangle(Bound, Color.Red * 0.5f);
            Container.Draw(spriteBatch, gameTime);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            Container.Update(gameTime);
        }
    }
}