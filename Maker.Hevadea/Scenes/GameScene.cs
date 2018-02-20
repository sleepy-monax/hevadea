using System.IO;
using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Entities.Creatures;
using Maker.Hevadea.Game.Entities.Furnitures;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Scenes.Menus;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.UI.Widgets;
using Maker.Utils.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maker.Hevadea.Scenes
{
    public class GameScene : Scene
    {
        private readonly GameManager _game;

        public GameScene(GameManager game)
        {
            _game = game;
            _game.CurrentMenuChange += Game_CurrentMenuChange;
            Container = new Panel();
        }

        private void Game_CurrentMenuChange(Menu oldMenu, Menu newMenu)
        {
            // Add the menu to the ui tree.
            var p = (Panel) Container;
            p.Content = newMenu;
            Container.RefreshLayout();
        }

        public override void Load()
        {
            _game.Initialize();
        }


        public override void OnUpdate(GameTime gameTime)
        {
            if (_game.CurrentMenu == null || !_game.CurrentMenu.PauseGame)
            {
                _game.Update(gameTime);
                var playerMovement = _game.Player.Components.Get<Move>();

                if (Engine.Input.KeyDown(Keys.Q)) playerMovement.Do(-1, 0, Direction.Left);
                if (Engine.Input.KeyDown(Keys.D)) playerMovement.Do(1, 0, Direction.Right);
                if (Engine.Input.KeyDown(Keys.Z)) playerMovement.Do(0, -1, Direction.Up);
                if (Engine.Input.KeyDown(Keys.S)) playerMovement.Do(0, 1, Direction.Down);
                if (Engine.Input.KeyPress(Keys.I)) _game.CurrentMenu = new InventoryMenu(_game.Player, RECIPIES.HandCrafted,_game);
                if (Engine.Input.KeyPress(Keys.N)) playerMovement.NoClip = !playerMovement.NoClip;

                var pos = _game.Player.GetFacingTile();
                if (Engine.Input.KeyPress(Keys.D1))
                {
                    var z = new ZombieEntity();
                    _game.Player.Level.SpawnEntity(z, pos.X, pos.Y);
                }

                if (Engine.Input.KeyPress(Keys.D2))
                {
                    var z = new ChestEntity();
                    _game.Player.Level.SpawnEntity(z, pos.X, pos.Y);
                }

                if (Engine.Input.KeyPress(Keys.D3))
                {
                    var z = new TorchEntity();
                    _game.Player.Level.SpawnEntity(z, pos.X, pos.Y);
                }

                if (Engine.Input.KeyDown(Keys.J)) _game.Player.Components.Get<Attack>().Do(_game.Player.HoldingItem);

                if (Engine.Input.KeyPress(Keys.K))
                {
                    if (_game.Player.Components.Get<Inventory>().Content.Count(_game.Player.HoldingItem) == 0)
                        _game.Player.HoldingItem = null;

                    _game.Player.Components.Get<Interact>().Do(_game.Player.HoldingItem);
                }
            }

            if (Engine.Input.KeyPress(Keys.Escape))
            {
                if (_game.CurrentMenu is HUDMenu)
                {
                    _game.CurrentMenu = new PauseMenu(_game);
                }
                else
                {
                    _game.CurrentMenu = new HUDMenu(_game);
                }
            }
        }

        public override void OnDraw(GameTime gameTime)
        {
            _game.Draw(gameTime);
        }

        public override void Unload()
        {
        }

        public override string GetDebugInfo()
        {
            return
                $@"World time: {_game.World.Time}
Player pos {_game.Player.X} {_game.Player.Y}
";
        }
    }
}