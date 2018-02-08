using System.Collections.Generic;
using Maker.Hevadea.Game.Entities.Component.Ai.Actions;
using Maker.Hevadea.Game.Entities.Component.Ai.Behavior;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component.Ai
{
    public abstract class Ai : EntityComponent, IUpdatableComponent, IDrawableComponent, IDrawableOverlayComponent
    {
        private readonly Stack<Action> _actionStack = new Stack<Action>();
        public BehaviorBase Behavior { get; set; }
        public Action CurrentAction => _actionStack.Peek();

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }


        public void Update(GameTime gameTime)
        {
        }

        public void PushAction(Action action)
        {
            _actionStack.Push(action);
        }
    }
}