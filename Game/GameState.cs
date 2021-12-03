using Hevadea.Entities;
using Hevadea.Framework;
using Hevadea.Framework.Data;
using Hevadea.Framework.Threading;
using Hevadea.Loading;
using Hevadea.Storage;
using Hevadea.Systems;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Hevadea.Framework.Extension;
using Hevadea.Scenes.Menus;

namespace Hevadea
{
    public class GameState
    {
        public delegate void CurrentMenuChangeHandler(Menu oldMenu, Menu newMenu);

        private Menu _currentMenu;
        private LevelSpriteBatchPool _spriteBatchPool = new LevelSpriteBatchPool();

        public string SavePath { get; set; } = "./test/";

        public Camera Camera { get; set; }
        public World World { get; set; }

        public PlayerSession LocalPlayer { get; set; }

        public List<EntitySystem> Systems { get; set; }

        public Menu CurrentMenu
        {
            get => _currentMenu;
            set
            {
                CurrentMenuChange?.Invoke(_currentMenu, value);
                _currentMenu = value;
            }
        }

        public event CurrentMenuChangeHandler CurrentMenuChange;
        
        public void Initialize()
        {
            Logger.Log<GameState>("Initializing...");
            World.Initialize(this);

            if (!Rise.NoGraphic)
                CurrentMenu = new MenuInGame(this);

            if (LocalPlayer != null)
            {
                LocalPlayer.Join(this);
                Camera = new Camera(LocalPlayer.Entity);
                Camera.JumpToFocusEntity();
            }
        }

        public void Draw(GameTime gameTime)
        {
            Camera.FocusEntity.Level.Draw(_spriteBatchPool, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);

            if (CurrentMenu?.PauseGame ?? false)
                return;

            LocalPlayer.Entity.Level.Update(gameTime);

            World.DayNightCycle.UpdateTime(gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void QuickSave()
        {
            var job = Jobs.SaveWorld.Then((_, e) => { CurrentMenu = new MenuInGame(this); });
            job.SetArguments(new Jobs.WorldSaveInfo(GetSavePath(), this));

            CurrentMenu = new LoadingMenu("Quick saving...", job, this);
        }

        public void SaveAndExit()
        {
            var job = Jobs.SaveWorld;
            job.SetArguments(new Jobs.WorldSaveInfo(GetSavePath(), this));
            job.Then((sender, e) => { Game.GoToMainMenu(); });

            CurrentMenu = new LoadingMenu("Saving and exiting...", job, this);
        }
        
        public string GetSavePath()
        {
            return SavePath.CombineWith($"/");
        }

        public string GetLevelSavePath(Level level)
        {
            return SavePath.CombineWith($"{level.Name}/");
        }

        public string GetLevelMinimapSavePath(Level level)
        {
            return SavePath.CombineWith($"{level.Name}/minimap.png");
        }

        public string GetLevelMinimapDataPath(Level level)
        {
            return SavePath.CombineWith($"{level.Name}/minimap.json");
        }
        
        public GameState Load(Job job, string saveFolder)
        {
            SavePath = saveFolder;

            job.Report("Loading world...");

            var path = GetSavePath();

            var worldStorage = File.ReadAllText(path + "world.json").FromJson<WorldStorage>();
            var world = World.Load(worldStorage);
            var player = PlayerSession.Load(File.ReadAllText(path + "player.json").FromJson<PlayerStorage>());

            foreach (var levelName in worldStorage.Levels) world.Levels.Add(LoadLevel(job, this, levelName));

            World = world;
            LocalPlayer = player;

            return this;
        }

        public Level LoadLevel(Job job, GameState gameState, string levelName)
        {
            var levelPath = $"{gameState.GetSavePath()}{levelName}/";
            var level = Level.Load(File.ReadAllText(levelPath + "level.json").FromJson<LevelStorage>());

            job.Report($"Loading level {level.Name}...");
            for (var x = 0; x < level.Chunks.GetLength(0); x++)
            for (var y = 0; y < level.Chunks.GetLength(1); y++)
            {
                level.Chunks[x, y] =
                    Chunk.Load(File.ReadAllText(levelPath + $"r{x}-{y}.json").FromJson<ChunkStorage>());
                job.Report((x * level.Chunks.GetLength(1) + y) / (float) level.Chunks.Length);
            }

            if (!Rise.NoGraphic)
            {
                level.Minimap.Waypoints =
                    File.ReadAllText(levelPath + "minimap.json").FromJson<List<MinimapWaypoint>>();

                var task = new Job((j, args) =>
                {
                    level.Minimap.LoadFromFile(levelPath + "minimap.png");
                    return null;
                });

                Rise.GameLoopThread.Enqueue(task);

                task.Wait();
            }

            return level;
        }

        public void Save(Job job, string savePath)
        {
            SavePath = savePath;

            job.Report("Saving world...");

            Directory.CreateDirectory(GetSavePath());

            foreach (var level in World.Levels) SaveLevel(job, level);

            File.WriteAllText(GetSavePath() + "world.json", World.Save().ToJson());
            File.WriteAllText(GetSavePath() + "player.json", LocalPlayer.Save().ToJson());
        }

        private void SaveLevel(Job job, Level level)
        {
            job.Report($"Saving {level.Name}...");
            var path = GetLevelSavePath(level);
            Directory.CreateDirectory(path);

            File.WriteAllText(path + "level.json", level.Save().ToJson());

            foreach (var chunk in level.Chunks)
            {
                job.Log(LoggerLevel.Info, $"Saving chunk {chunk.X}-{chunk.Y}...");
                job.Report((chunk.X * level.Chunks.GetLength(1) + chunk.Y) / (float) level.Chunks.Length);
                File.WriteAllText(path + $"r{chunk.X}-{chunk.Y}.json", chunk.Save().ToJson());
            }

            File.WriteAllText(path + "minimap.json", level.Minimap.Waypoints.ToJson());

            var task = new Job((j, args) =>
            {
                level.Minimap.SaveToFile(path + "minimap.png");
                return null;
            });

            job.Report($"Saving {level.Name} minimap...");
            job.Report(1f);

            Rise.GameLoopThread.Enqueue(task);

            task.Wait();
        }
    }
}