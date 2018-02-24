using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Creatures;
using Maker.Hevadea.Game.Storage;
using Maker.Hevadea.Scenes.Menus;
using Maker.Utils;
using Maker.Utils.Enums;
using Maker.Utils.Json;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game
{
    public class GameManager
    {
        private Menu _currentMenu;
        
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

        public void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, Camera);
        }

        public void Update(GameTime gameTime)
        {

            foreach (var l in World.Levels)
            {
                var state = l.GetRenderState(Camera);
                l.Update(state, gameTime);
            }

            World.Time += gameTime.ElapsedGameTime.TotalSeconds;
        }


        public static void Save(string path, GameManager game)
        {
            new GameSaver().Save(path,game);
        }

        public static GameManager Load(string path)
        {
            return new GameLoader().Load(path);
        }
    }
}