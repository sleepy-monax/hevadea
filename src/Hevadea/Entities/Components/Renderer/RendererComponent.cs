using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Entities.Components.Renderer
{
    public abstract class RendererComponent : EntityComponent
    {
        public abstract void Draw(SpriteBatch spriteBatch, Entity entity, Vector2 position, GameTime gameTime);
    }
}
