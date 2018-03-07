using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Game.Entities.Components.Ai
{
    public class Agent: EntityComponent, IEntityComponentUpdatable
    {
        public Queue<AiAction> ActionQueue { get; } = new Queue<AiAction>();

        public void Update(GameTime gameTime)
        {
           
        }
    }
}