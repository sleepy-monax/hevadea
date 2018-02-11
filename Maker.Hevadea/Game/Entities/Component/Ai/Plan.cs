using System.Collections.Generic;
using Maker.Hevadea.Game.Entities.Component.Ai.Actions;

namespace Maker.Hevadea.Game.Entities.Component.Ai
{
    public class Plan
    { 
        public List<Action> Actions { get; set; } = new List<Action>();

        public bool IsValid(Entity e)
        {
            var result = true;

            foreach (var a in Actions)
            {
                result &= a.IsValid(e);
            }

            return result;
        }
        
        public int GetCost(Entity e)
        {
            var result = 0;

            foreach (var a in Actions)
            {
                result+= a.GetCost(e);
            }

            return result;
        }
    }
}