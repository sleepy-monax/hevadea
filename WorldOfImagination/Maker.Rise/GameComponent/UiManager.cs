using Maker.Rise.GameComponent.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.GameComponent
{
    public class UiManager : GameComponent
    {
        private SpriteBatch uiSpriteBatch;
        
        public InputManager Input => Game.Input;
        public Ressources Ress => Game.Ress;
        public bool Debug => Game.Debug.Visible;
        
        public UiManager(WorldOfImaginationGame game) : base(game)
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
            uiSpriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, Game.RasterizerState);
            UiRoot.Draw(uiSpriteBatch, gameTime);
            uiSpriteBatch.End();
        }
    }
}