using Maker.Rise;
using Maker.Rise.GameComponent;
using Maker.Rise.GameComponent.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using WorldOfImagination.Game;
using WorldOfImagination.Game.UI;

namespace WorldOfImagination.Scenes
{
    public class GameScene : Scene
    {

        private SpriteBatch spriteBatch;
        public World World;
        private bool showDebug = false;

        public GameScene(RiseGame game, World world) : base(game)
        {
            World = world;
        }
        
        public override void Load()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            GenerateLevel(0);
            World.Initialize();

            UiRoot.Childs = new List<Control>
            {
                new PlayerInfoPanel(Game.UI, World.Player){ Dock = Dock.Top, Bound = new Rectangle(64, 64, 64, 72)},
            };
        }

        public void GenerateLevel(int seed)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Game.Input.KeyPress(Keys.F4)) showDebug = !showDebug;

            World.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, showDebug);
        }

        public override void Unload()
        {
            
        }
    }
}