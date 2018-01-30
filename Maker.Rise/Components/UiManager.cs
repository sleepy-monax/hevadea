using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.Components
{
    public class UiManager : GameComponent
    {
        private SpriteBatch uiSpriteBatch;
        public bool Debug => Engine.Debug.Visible;

        public UiManager(InternalGame game) : base(game)
        {
        }

        public override void Initialize()
        {
            uiSpriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void DrawUiTree(GameTime gameTime, Control UiRoot)
        {
            uiSpriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, Engine.CommonRasterizerState);
            UiRoot.Draw(uiSpriteBatch, gameTime);
            uiSpriteBatch.End();
        }
    }
}