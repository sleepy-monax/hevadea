using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Component.Ai
{
    public abstract class StateBahevior
    {
        public abstract bool IsValid<T>(Agent<T> agent);
        public abstract void Update<T>(Agent<T> agent, GameTime gameTime);
    }
}