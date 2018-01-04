using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
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
            World.Initialize();

            UiRoot.Childs = new List<Control>
            {
                new PlayerInfoPanel(World.Player){ Dock = Dock.Top, Bound = new Rectangle(64, 64, 64, 72)},
            };
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

            if (Engine.Input.KeyDown(Keys.Q)) { World.Player.Move(-1, 0, Direction.Left); }
            if (Engine.Input.KeyDown(Keys.D)) { World.Player.Move(1, 0, Direction.Right); }
            if (Engine.Input.KeyDown(Keys.Z)) { World.Player.Move(0, -1, Direction.Up); }
            if (Engine.Input.KeyDown(Keys.S)) { World.Player.Move(0, 1, Direction.Down); }

            if (Engine.Input.KeyPress(Keys.N)) { World.Player.NoClip = !World.Player.NoClip; }

            if (Engine.Input.MouseLeft) World.Player.Attack(World.Player.HoldingItem);
            if (Engine.Input.MouseRight) World.Player.Use(World.Player.HoldingItem);
        }

        public override void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, showDebug, renderTiles, renderEntities);
        }

        public override void Unload()
        {
            
        }

        public override string GetDebugInfo()
        {
            return 
$@"World time: {World.Time}
Player pos {World.Player.X} {World.Player.Y}
";
        }
    }
}