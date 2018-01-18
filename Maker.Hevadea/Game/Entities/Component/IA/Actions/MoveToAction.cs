using Maker.Hevadea.Game.Entities.Component.Misc;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.IA.Actions
{
    public class MoveToAction : Action
    {
        public Entity Objective;
        public float Speed;
        
        public MoveToAction(Entity objective, float speed, AiBaseComponent ai) : base(ai)
        {
            Objective = objective;
            Speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            var move = Owner.Components.Get<MoveComponent>();
            
            if (move != null)
            {

            }
            else
            {
                Ai.NextAction();
            }
        }
    }
}