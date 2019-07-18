using Hevadea.Entities;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public abstract class EntityDrawSystem : EntitySystem
    {
        public abstract void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime);
    }

    public abstract class EntityUpdateSystem : EntitySystem
    {
        public abstract void Update(Entity entity, GameTime gameTime);
    }

    public class EntitySystem
    {
        public bool Enable { get; set; } = true;
        public Filter Filter { get; private set; } = new Filter();
    }
}