using Maker.Hevadea.Game.Entities.Component.Misc;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.IA.Actions
{
    public class Action
    {
        public AiBaseComponent Ai;
        public Entity Owner { get; }

        public Action(AiBaseComponent ai)
        {
            Ai = ai;
            Owner = ai.Owner;
        }
        
        public virtual void Update(GameTime gameTime)
        {

        }
    }
}