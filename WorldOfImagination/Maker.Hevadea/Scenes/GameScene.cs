using Maker.Hevadea.Enum;
using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Menus;
using Maker.Hevadea.Game.UI;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Control = Maker.Rise.UI.Control;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Maker.Hevadea.Scenes
{
    public class GameScene : Scene
    {
        private SpriteBatch spriteBatch;
        public World World;
        public Menu CurrentMenu = null;

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
                new PlayerInfoPanel(World.Player) {Dock = Dock.Top, Bound = new Rectangle(64, 64, 64, 72)},
            };
        }


        public override void Update(GameTime gameTime)
        {
            // Update the world.
            if (CurrentMenu == null || !CurrentMenu.PauseGame)
            {
                World.Update(gameTime);
            }

            var playerMovement = World.Player.GetComponent<MoveComponent>();
            
            // Player mouvement.
            if (Engine.Input.KeyDown(Keys.Q))
            {
                playerMovement.Move(-1, 0, Direction.Left);
            }

            if (Engine.Input.KeyDown(Keys.D))
            {
                playerMovement.Move(1, 0, Direction.Right);
            }

            if (Engine.Input.KeyDown(Keys.Z))
            {
                playerMovement.Move(0, -1, Direction.Up);
            }

            if (Engine.Input.KeyDown(Keys.S))
            {
                playerMovement.Move(0, 1, Direction.Down);
            }

            if (Engine.Input.KeyPress(Keys.N))
            {
                World.Player.NoClip = !World.Player.NoClip;
            }

            if (Engine.Input.MouseLeft) World.Player.GetComponent<AttackComponent>().Attack(World.Player.HoldingItem);
            // TODO use component
            //if (Engine.Input.MouseRight) World.Player.Use(World.Player.HoldingItem);
        }

        public override void Draw(GameTime gameTime)
        {
            World.Draw(gameTime);
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