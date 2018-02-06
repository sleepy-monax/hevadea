using System.Collections.Generic;
using Maker.Hevadea.Game.Entities.Component.Ai.Actions;
using Maker.Hevadea.Game.Entities.Component.Ai.Behavior;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Ai
{
    public class Ai : EntityComponent, IUpdatableComponent, IDrawableComponent, IDrawableOverlayComponent
    {

        public Stack<Action> ActionStack;
        public BehaviorBase Behavior;
        
        public void NextAction()
        {
            
        }
        
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
    }
}
