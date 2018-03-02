using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Attributes;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Entities.Furnitures;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Menus;
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
                var playerMovement = _game.Player.Get<Move>();

                float x = 0;
                float y = 0;
                Direction dir = Direction.Down;
                
                if (Rise.Input.KeyDown(Keys.Q))
                {
                    x -= 1;
                    dir = Direction.Left;
                }
                if (Rise.Input.KeyDown(Keys.D))
                {
                    x += 1;
                    dir = Direction.Right;
                }
                if (Rise.Input.KeyDown(Keys.Z))
                {
                    y -= 1;
                    dir = Direction.Up;
                }
                if (Rise.Input.KeyDown(Keys.S))
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
                
                if (Rise.Input.KeyPress(Keys.E)) _game.CurrentMenu = new InventoryMenu(_game.Player, RECIPIES.HandCrafted,_game);
                if (Rise.Input.KeyPress(Keys.N)) playerMovement.NoClip = !playerMovement.NoClip;

                var pos = _game.Player.GetFacingTile();
                if (Rise.Input.KeyPress(Keys.D1))
                {
                    var z = new ZombieEntity();
                    _game.Player.Level.SpawnEntity(z, pos.X, pos.Y);
                }

                if (Rise.Input.KeyPress(Keys.D2))
                {
                    var z = new ChestEntity();
                    _game.Player.Level.SpawnEntity(z, pos.X, pos.Y);
                }

                if (Rise.Input.KeyPress(Keys.D3))
                {
                    var z = new TorchEntity();
                    _game.Player.Level.SpawnEntity(z, pos.X, pos.Y);
                }

                if (Rise.Input.KeyPress(Keys.Up))
                {
                    _game.Camera.Zoom *= 2;
                }
                
                if (Rise.Input.KeyPress(Keys.Down))
                {
                    _game.Camera.Zoom /= 2;
                }
                
                if (Rise.Input.KeyDown(Keys.J)) _game.Player.Get<Attack>().Do(_game.Player.HoldingItem);

                if (Rise.Input.KeyPress(Keys.K))
                {
                    if (_game.Player.Get<Inventory>().Content.Count(_game.Player.HoldingItem) == 0)
                        _game.Player.HoldingItem = null;

                    _game.Player.Get<Interact>().Do(_game.Player.HoldingItem);
                }
            }

            if (Rise.Input.KeyPress(Keys.Escape))
            {
                if (_game.CurrentMenu is HudMenu)
                {
                    _game.CurrentMenu = new PauseMenu(_game);
                }
                else
                {
                    _game.CurrentMenu = new HudMenu(_game);
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
                $@"World time: {_game.World.DayNightCycle.Time}
Time of the day: {_game.World.DayNightCycle.TimeOfTheDay} / {_game.World.DayNightCycle.CycleDuration}
Days : {_game.World.DayNightCycle.DayCount}
Current Stage: {_game.World.DayNightCycle.GetCurrentStage().Name} : {(int)_game.World.DayNightCycle.GetTimeOfTheCurrentStage()}/{(int)_game.World.DayNightCycle.GetCurrentStage().Duration}
Player pos {_game.Player.X} {_game.Player.Y}
";
        }
    }
}