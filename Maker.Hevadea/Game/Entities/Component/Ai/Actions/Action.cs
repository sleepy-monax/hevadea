using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.Ai.Actions
{
    public class Action
    {
        public Ai Ai;
        public Entity Owner { get; }

        public Action(Ai ai)
        {
            Ai = ai;
            Owner = ai.Owner;
        }
        
        public virtual void Update(GameTime gameTime)
        {

        }
    }
}