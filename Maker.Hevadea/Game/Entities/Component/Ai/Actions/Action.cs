using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.Ai.Actions
{
    public abstract class Action
    {
        public Ai Ai;

        public Action(Ai ai)
        {
            Ai = ai;
            Owner = ai.Owner;
        }

        public Entity Owner { get; }

        public abstract bool IsRunning();


        public virtual void Update(GameTime gameTime)
        {
        }
    }
}