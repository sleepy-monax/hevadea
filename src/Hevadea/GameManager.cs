using Hevadea.Framework;
using Hevadea.Framework.Threading;
using Hevadea.Framework.Utils.Json;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.Player;
using Hevadea.Scenes.Menus;
using Hevadea.Storage;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Hevadea
{
    public class GameManager
    {
        Menu _currentMenu;

        public string SavePath { get; set; } = "./test/";
        public World World { get; set; }
        public EntityPlayer MainPlayer { get; set; }
        public List<EntityPlayer> Players { get; } = new List<EntityPlayer>();

        public Camera Camera { get; set; }
        public PlayerInputHandler PlayerInput { get; set; }

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

        public GameManager()
        {
        }

        public GameManager(World world, EntityPlayer player)
        {
            World = world;
            MainPlayer = player;
        }

        public void Initialize()
        {
            World.Initialize(this);
            CurrentMenu = new MenuInGame(this);

            if (MainPlayer.Removed)
            {
                if (MainPlayer.X == 0f && MainPlayer.Y == 0f)
                    World.SpawnPlayer(MainPlayer);
                else
                    World.GetLevel(MainPlayer.LastLevel).AddEntity(MainPlayer);
            }

            PlayerInput = new PlayerInputHandler(MainPlayer);
            Camera = new Camera(MainPlayer);
            Camera.JumpToFocusEntity();
        }
        
		public static GameManager Load(string saveFolder, ProgressRepporter progressRepporter)
        {
			GameManager game = new GameManager();
			game.SavePath = saveFolder;
            
			progressRepporter.RepportStatus("Loading world...");
            
            // Load the world data and the player.
            string path = game.GetSavePath();

            WorldStorage worldStorage = File.ReadAllText(path + "world.json").FromJson<WorldStorage>();
            World world = World.Load(worldStorage);
            Entity player = EntityFactory.PLAYER.Construct().Load(File.ReadAllText(path + "player.json").FromJson<EntityStorage>());

            // Load levels of the world.
            foreach (var levelName in worldStorage.Levels)
            {
                // Load level data
                string levelPath = $"{path}{levelName}/";
                Level level = Level.Load(File.ReadAllText(levelPath + "level.json").FromJson<LevelStorage>());

                // Load chunks
				progressRepporter.RepportStatus($"Loading level {level.Name}...");
                for (int x = 0; x < level.Chunks.GetLength(0); x++)
                {
                    for (int y = 0; y < level.Chunks.GetLength(1); y++)
                    {
                        level.Chunks[x, y] = Chunk.Load(File.ReadAllText(levelPath + $"r{x}-{y}.json").FromJson<ChunkStorage>());
						progressRepporter.Report((x * level.Chunks.GetLength(1) + y) / (float)level.Chunks.Length);
                    }
                }

                // Load the minimap.
                level.Minimap.Waypoints = File.ReadAllText(levelPath + "minimap.json").FromJson<List<MinimapWaypoint>>();

                var task = new AsyncTask(() =>
                {
                    var fs = new FileStream(levelPath + "minimap.png", FileMode.Open);
                    level.Minimap.Texture = Texture2D.FromStream(Rise.MonoGame.GraphicsDevice, fs);
                    fs.Close();
                });

                Rise.AsyncTasks.Enqueue(task);

                while (!task.Done)
                {
                    // XXX: Hack to fix the soft lock when loading the world.
                    System.Threading.Thread.Sleep(10);
                }

                world.Levels.Add(level);
            }

            game.World = world;
            game.MainPlayer = (EntityPlayer)player;

			return game;
        }

		public void Save(string savePath, ProgressRepporter progressRepporter)
        {
			SavePath = savePath;

			progressRepporter.RepportStatus("Saving world...");
            
            var levelsName = new List<string>();

			Directory.CreateDirectory(SavePath);

            // Saves levels
            foreach (var level in World.Levels)
            {
				progressRepporter.RepportStatus($"Saving {level.Name}...");
                string path = GetLevelSavePath(level);
                Directory.CreateDirectory(path);
                
                // Save the level information
                File.WriteAllText(path + "level.json", level.Save().ToJson());

                // Save chunks
                foreach (var chunk in level.Chunks)
                {
					progressRepporter.Report((chunk.X * level.Chunks.GetLength(1) + chunk.Y) / (float)level.Chunks.Length);
                    File.WriteAllText(path + $"r{chunk.X}-{chunk.Y}.json", chunk.Save().ToJson());
                }
                
                // Save the minimap
                File.WriteAllText(path + "minimap.json", level.Minimap.Waypoints.ToJson());

                // We can only save the minimap texture in the render thread.
                var task = new AsyncTask(() =>
                {
                    var fs = new FileStream(path + "minimap.png", FileMode.OpenOrCreate);
                    level.Minimap.Texture.SaveAsPng(fs, level.Width, level.Height);
                    fs.Close();
                });

				progressRepporter.RepportStatus($"Saving {level.Name} minimap...");
				progressRepporter.Report(1f);
                Rise.AsyncTasks.Enqueue(task);

                // Wait for the task to complete.
                while (!task.Done)
                {
                    // XXX: Hack to fix the soft lock when saving the world.
                    System.Threading.Thread.Sleep(10);
                }
            }

            File.WriteAllText(GetSavePath() + "world.json", World.Save().ToJson());
            File.WriteAllText(GetSavePath() + "player.json", MainPlayer.Save().ToJson()); 
        }

        public string GetSavePath()
            => $"{SavePath}/";
        
        public string GetLevelSavePath(Level level)
            => $"{SavePath}/{level.Name}/";

        public string GetLevelMinimapSavePath(Level level)
            => $"{SavePath}/{level.Name}/minimap.png";

        public string GetLevelMinimapDataPath(Level level)
            => $"{SavePath}/{level.Name}/minimap.json";

        public string GetLevelSavePath(string level)
            => $"{SavePath}/{level}.json";

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