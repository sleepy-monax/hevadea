using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Menus;
using Maker.Hevadea.Menus;
using Microsoft.Xna.Framework;
using System;

namespace Maker.Hevadea.Game
{
    public class GameManager
    {
        public World World { get; private set; }
        public PlayerEntity Player { get; private set; }
        public Camera Camera { get; private set; }
        public Random Random { get; private set; } = new Random();

        private Menu _currentMenu;
        public int Time { get; set; } = 0;

        public GameManager(World world, PlayerEntity player)
        {
            World = world;
            Player = player;
            Camera = new Camera(Player);
        }

        public void Initialize()
        {
            World.Initialize(this);
            CurrentMenu = new HUDMenu(this);
            World.SpawnPlayer(Player);
        }

        public Menu CurrentMenu
        {
            get => _currentMenu;
            set 
            {
                CurrentMenuChange?.Invoke(_currentMenu, value);
                _currentMenu = value;
            }
        }

        public delegate void CurrentMenuChangeHandler(Menu oldMenu, Menu newMenu);
        public event CurrentMenuChangeHandler CurrentMenuChange;

        public void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, Camera);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var l in World.Levels)
            {
                l.Update(gameTime);
            }

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
