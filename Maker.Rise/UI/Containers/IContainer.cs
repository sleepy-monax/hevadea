using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Containers
{
    public interface IContainer
    {
        List<Control> Childs { get; set; }
        void RefreshLayout(Rectangle host);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}