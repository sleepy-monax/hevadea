using Maker.Rise;
using Maker.Rise.GameComponent;
using Maker.Rise.GameComponent.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using WorldOfImagination.Game;
using WorldOfImagination.Game.Menus;
using WorldOfImagination.Game.UI;
using Control = Maker.Rise.GameComponent.UI.Control;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace WorldOfImagination.Scenes
{
    public class GameScene : Scene
    {

        private SpriteBatch spriteBatch;
        public World World;
        private bool showDebug = false;
        private bool renderTiles = true;
        private bool renderEntities = true;
        public Menu currentMenu = null;
        
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
            if (Game.Input.KeyDown(Keys.F4))
            {
                if (Game.Input.KeyPress(Keys.D))
                {
                    showDebug = !showDebug;
                }
                if (Game.Input.KeyPress(Keys.T))
                {
                    renderTiles = !renderTiles;
                }
                if (Game.Input.KeyPress(Keys.E))
                {
                    renderEntities = !renderEntities;
                }
            }

            if (currentMenu == null || !currentMenu.PauseGame)
            {
                World.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, showDebug, renderTiles, renderEntities);
        }

        public override void Unload()
        {
            
        }
    }
}