using Maker.Hevadea.Enums;
using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Menus;
using Maker.Hevadea.Menus;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Enums;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maker.Hevadea.Scenes
{
    public class GameScene : Scene
    {
        public GameManager Game;

        public GameScene(GameManager game)
        {
            Game = game;
            Game.CurrentMenuChange += Game_CurrentMenuChange;
        }

        private void Game_CurrentMenuChange(Menu oldMenu, Menu newMenu)
        {
            if (oldMenu != null)
            {
                UiRoot.RemoveChild(oldMenu);
            }

            // Add the menu to the ui tree.
            newMenu.Dock = Dock.Fill;
            UiRoot.AddChild(newMenu);
            UiRoot.RefreshLayout();
        }

        public override void Load()
        {
            Game.Initialize();
            UiRoot.Padding = new Padding(16);
            Engine.Scene.Background = null;

        }


        public override void Update(GameTime gameTime)
        {
            if (Game.CurrentMenu == null || !Game.CurrentMenu.PauseGame)
            {
                Game.Update(gameTime);


                var playerMovement = Game.Player?.Components.Get<Move>();
            
                if (Engine.Input.KeyDown(Keys.Q))  { playerMovement.Do(-1, 0, Direction.Left); }
                if (Engine.Input.KeyDown(Keys.D))  { playerMovement.Do(1, 0, Direction.Right); }
                if (Engine.Input.KeyDown(Keys.Z))  { playerMovement.Do(0, -1, Direction.Up);   }
                if (Engine.Input.KeyDown(Keys.S))  { playerMovement.Do(0, 1, Direction.Down);  }
                if (Engine.Input.KeyPress(Keys.I)) { Game.CurrentMenu = new InventoryMenu(Game.Player, Game); }
                if (Engine.Input.KeyPress(Keys.N)) { playerMovement.NoClip = !playerMovement.NoClip; }

                var pos = Game.Player.GetFacingTile();
                if (Engine.Input.KeyPress(Keys.D1)) {var z = new ZombieEntity(); Game.Player.Level.SpawnEntity(z, pos.X, pos.Y); }
                if (Engine.Input.KeyPress(Keys.D2)) { var z = new ChestEntity(); Game.Player.Level.SpawnEntity(z, pos.X, pos.Y); }
                if (Engine.Input.KeyPress(Keys.D3)) { var z = new TorchEntity(); Game.Player.Level.SpawnEntity(z, pos.X, pos.Y); }
                
                if (Engine.Input.MouseLeft) Game.Player.Components.Get<Attack>().Do(Game.Player.HoldingItem);

                if (Engine.Input.MouseRightClick)
                {
                    if (Game.Player.Components.Get<Inventory>().Content.Count(Game.Player.HoldingItem) == 0)
                    {
                        Game.Player.HoldingItem = null;
                    }

                    Game.Player.Components.Get<Interact>().Do(Game.Player.HoldingItem);
                }

                //if (Engine.Input.KeyPress(Keys.P)) { File.WriteAllText("test.json", World[0].Save().ToJson()); }
            }

            if (Engine.Input.KeyPress(Keys.Escape) && Game.CurrentMenu != null)
            {
                Game.CurrentMenu = new HUDMenu(Game);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.Draw(gameTime);
        }

        public override void Unload()
        {
        }

        public override string GetDebugInfo()
        {
            return
                $@"World time: {Game.World.Time}
Player pos {Game.Player.X} {Game.Player.Y}
";
        }
    }
}