using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Storage;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using System;
using Hevadea.Game.Worlds;

namespace Hevadea.Game
{
    public class GameManager
    {
        private Menu _currentMenu;

        public World World { get; }
        public PlayerEntity Player { get; }
        public Camera Camera { get; }
        public Random Random { get; } = new Random();
        public PlayerInputHandler PlayerInput { get; }

        public delegate void CurrentMenuChangeHandler(Menu oldMenu, Menu newMenu);
        public event CurrentMenuChangeHandler CurrentMenuChange;
        
        public Menu CurrentMenu
        {
            get => _currentMenu;
            set
            {
                CurrentMenuChange?.Invoke(_currentMenu, value);
                _currentMenu = value;
            }
        }

        public GameManager(World world, PlayerEntity player)
        {
            World = world;
            Player = player;
            Camera = new Camera(Player);
            PlayerInput = new PlayerInputHandler(player);
        }

        public void Initialize()
        {
            World.Initialize(this);
            CurrentMenu = new HudMenu(this);
            World.SpawnPlayer(Player);
        }

        public void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, Camera);
        }

        public void Update(GameTime gameTime)
        {
            if (!CurrentMenu?.PauseGame ?? false)
            {
                Camera.Update(gameTime);
                PlayerInput.Update(gameTime);
            
                foreach (var l in World.Levels)
                {
                    var state = l.GetRenderState(Camera);
                    l.Update(state, gameTime);
                }
            }

            World.DayNightCycle.UpdateTime(gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}