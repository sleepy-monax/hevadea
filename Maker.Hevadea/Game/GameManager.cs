using System;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Creatures;
using Maker.Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game
{
    public class GameManager
    {
        private Menu _currentMenu;
        private string _saveName;
        
        public World World { get; }
        public PlayerEntity Player { get; }
        public Camera Camera { get; }
        public Random Random { get; } = new Random();
        
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

        public GameManager(string saveName, World world, PlayerEntity player)
        {
            _saveName = saveName;
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

        public void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, Camera);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var l in World.Levels) l.Update(gameTime);

            World.Time += gameTime.ElapsedGameTime.TotalSeconds;
            
        }


        public void Save()
        {
            World.Save();
        }

        public void Load()
        {
        }
    }
}