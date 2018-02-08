using System;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game
{
    public class GameManager
    {
        public delegate void CurrentMenuChangeHandler(Menu oldMenu, Menu newMenu);

        private Menu _currentMenu;

        public GameManager(World world, PlayerEntity player)
        {
            World = world;
            Player = player;
            Camera = new Camera(Player);
        }

        public World World { get; }
        public PlayerEntity Player { get; }
        public Camera Camera { get; }
        public Random Random { get; } = new Random();
        public int Time { get; set; } = 0;

        public Menu CurrentMenu
        {
            get => _currentMenu;
            set
            {
                CurrentMenuChange?.Invoke(_currentMenu, value);
                _currentMenu = value;
            }
        }

        public void Initialize()
        {
            World.Initialize(this);
            CurrentMenu = new HUDMenu(this);
            World.SpawnPlayer(Player);
        }

        public event CurrentMenuChangeHandler CurrentMenuChange;

        public void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, Camera);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var l in World.Levels) l.Update(gameTime);

            World.Time++;
        }


        public static void Save()
        {
        }

        public static void Load()
        {
        }
    }
}