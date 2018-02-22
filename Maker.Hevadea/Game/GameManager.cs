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
            Logger.Log<GameManager>(LoggerLevel.Info, $"Saving world to '{path}'.");
            
            Directory.CreateDirectory(path);
            var p = game.Player;
            var player = p.Save();
            p.Level.RemoveEntity(p);
            var world = game.World.Save();
            p.Level.AddEntity(p);
            
            File.WriteAllText($"{path}\\world.json", world.ToJson());
            File.WriteAllText($"{path}\\player.json", player.ToJson());
            
            Logger.Log<GameManager>(LoggerLevel.Fine, "Done!");
        }

        public static GameManager Load(string path)
        {
            Logger.Log<GameManager>(LoggerLevel.Info, $"Loading world from '{path}'.");
            
            var world = new World();
            world.Load(File.ReadAllText($"{path}\\world.json").FromJson<WorldStorage>());
                
            var player = new PlayerEntity();
            player.Load(File.ReadAllText($"{path}\\player.json").FromJson<EntityStorage>());
            
            Logger.Log<GameManager>(LoggerLevel.Fine, "Done!");
            
            return new GameManager(world, player);
        }
    }
}