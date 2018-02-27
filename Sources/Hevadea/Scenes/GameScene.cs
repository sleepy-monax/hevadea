using Hevadea.Game;
using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Entities.Furnitures;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Menus;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes
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

                float x = 0;
                float y = 0;
                Direction dir = Direction.Down;
                
                if (Engine.Input.KeyDown(Keys.Q))
                {
                    x -= 1;
                    dir = Direction.Left;
                }
                if (Engine.Input.KeyDown(Keys.D))
                {
                    x += 1;
                    dir = Direction.Right;
                }
                if (Engine.Input.KeyDown(Keys.Z))
                {
                    y -= 1;
                    dir = Direction.Up;
                }
                if (Engine.Input.KeyDown(Keys.S))
                {
                    y += 1;
                    dir = Direction.Down;
                }

                if (x != 0 || y != 0)
                {
                    var vec = new Vector2(x, y);
                    vec.Normalize();
                    vec = vec * 1.5f;
                    playerMovement.Do(vec.X, vec.Y, dir);
                }
                
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

                if (Engine.Input.KeyPress(Keys.Up))
                {
                    _game.Camera.Zoom *= 2;
                }
                
                if (Engine.Input.KeyPress(Keys.Down))
                {
                    _game.Camera.Zoom /= 2;
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
                $@"World time: {_game.World.DayNightCicle.Time}
Time of the day: {_game.World.DayNightCicle.TimeOfTheDay} / {_game.World.DayNightCicle.CicleDuration}
Days : {_game.World.DayNightCicle.DayCount}
Current Stage: {_game.World.DayNightCicle.GetCurrentStage().Name} : {(int)_game.World.DayNightCicle.GetTimeOfTheCurrentStage()}/{(int)_game.World.DayNightCicle.GetCurrentStage().Duration}
Player pos {_game.Player.X} {_game.Player.Y}
";
        }
    }
}