using Hevadea.Game.Entities;
using Hevadea.Game.Worlds;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using System;

namespace Hevadea.Game
{
    public class GameManager
    {
        private Menu _currentMenu;

        public World World { get; }
        public EntityPlayer Player { get; }
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

        public GameManager(World world, EntityPlayer player)
        {
            World = world;
            Player = player;
            Camera = new Camera(Player);
            PlayerInput = new PlayerInputHandler(player);
        }

        public void Initialize()
        {
            World.Initialize(this);
            CurrentMenu = new MenuInGame(this);
            World.SpawnPlayer(Player);
        }

        public void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, Camera);
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            PlayerInput.Update(gameTime);
            
            if (!CurrentMenu?.PauseGame ?? false)
            {
            
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