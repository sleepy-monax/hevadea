using Maker.Rise.UI.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponent = Maker.Rise.Components.GameComponent;

namespace Maker.Rise.UI
{
    public class UiManager : GameComponent
    {
        private SpriteBatch _spriteBatch;
        public bool Debug => Engine.Debug.Visible;

        public UiManager(InternalGame game) : base(game)
        {
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void DrawUiTree(GameTime gameTime, IContainer container)
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, Engine.CommonRasterizerState);
            container.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
        }
    }
}