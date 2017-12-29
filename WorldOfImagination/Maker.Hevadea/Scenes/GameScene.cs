using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Menus;
using Maker.Hevadea.Game.UI;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Control = Maker.Rise.UI.Control;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Maker.Hevadea.Scenes
{
    public class GameScene : Scene
    {

        private SpriteBatch spriteBatch;
        private SplashScene lightSpriteBatch;
        public World World;
        private bool showDebug = false;
        private bool renderTiles = true;
        private bool renderEntities = true;
        public Menu currentMenu = null;
        
        public GameScene(World world)
        {
            World = world;
        }
        
        public override void Load()
        {
            spriteBatch = new SpriteBatch(Engine.Graphic.GraphicsDevice);
            GenerateLevel(0);
            World.Initialize();

            UiRoot.Childs = new List<Control>
            {
                new PlayerInfoPanel(World.Player){ Dock = Dock.Top, Bound = new Rectangle(64, 64, 64, 72)},
            };
        }

        public void GenerateLevel(int seed)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Engine.Input.KeyDown(Keys.F4))
            {
                if (Engine.Input.KeyPress(Keys.D))
                {
                    showDebug = !showDebug;
                }
                if (Engine.Input.KeyPress(Keys.T))
                {
                    renderTiles = !renderTiles;
                }
                if (Engine.Input.KeyPress(Keys.E))
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