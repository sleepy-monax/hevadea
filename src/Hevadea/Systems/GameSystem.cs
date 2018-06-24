using Hevadea.Entities;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public interface IEntityProcessSystem
    {
        void Process(Entity entity, GameTime gameTime);
    }

    public interface IEntityRenderSystem
    {
        void Render(Entity entity, LevelSpriteBatchPool pool,  GameTime gameTime);
    }

    public class GameSystem 
    {
        public bool Enable { get; set; } = true;
		public Filter Filter { get; set; } = new Filter();
    }
}
