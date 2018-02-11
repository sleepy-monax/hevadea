using System.Collections.Generic;
using Maker.Hevadea.Game.Entities.Component.Ai.Actions;

namespace Maker.Hevadea.Game.Entities.Component.Ai
{
    public class Goal
    {
        public bool Enable { get; set; } = true;
        public List<Plan> Plans { get; set; } = new List<Plan>();
        private string _description;
        
        public Goal(string desciption)
        {
            _description = desciption;
        }


    }
}