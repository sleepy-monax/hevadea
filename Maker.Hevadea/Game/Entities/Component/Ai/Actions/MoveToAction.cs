using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.Ai.Actions
{
    public class MoveToAction : Action
    {
        public Entity Objective;
        public float Speed;
        
        public MoveToAction(Entity objective, float speed, Ai ai) : base(ai)
        {
            Objective = objective;
            Speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            var move = Owner.Components.Get<Move>();
            
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