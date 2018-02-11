using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.Ai.Actions
{
    public abstract class Action
    {
        public abstract bool IsValid(Entity e);
        public abstract int  GetCost(Entity e);
        public abstract bool Do(Entity e, GameTime gameTime);
    }
}