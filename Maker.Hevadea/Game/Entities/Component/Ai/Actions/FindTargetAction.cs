using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.Ai.Actions
{
    public class FindTargetAction : Action
    {
        public delegate bool FindTargetPredicat(Entity e);
        private FindTargetPredicat _predicat;
        
        public FindTargetAction(FindTargetPredicat predicat)
        {
            _predicat = predicat;
        }

        public override bool IsValid(Entity e)
        {
            throw new System.NotImplementedException();
        }

        public override int GetCost(Entity e)
        {
            throw new System.NotImplementedException();
        }

        public override bool Do(Entity e, GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}