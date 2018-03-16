using Hevadea.Framework.Utils;
using Hevadea.Framework.Utils.Json;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Hevadea.Game.Worlds;
using System.Collections.Generic;
using System.IO;

namespace Hevadea.Game.Loading.Tasks
{
    public class TaskSaveWorld : LoadingTask
    {
        public override void Task(GameManager game)
        {
            SetStatus("Saving world...");

            var levelsName = new List<string>();

            Directory.CreateDirectory(game.SavePath);
            
            // Saves levels
            
            foreach (var level in game.World.Levels)
            {
                SetStatus($"Saving level the '{level.Name}'...");
                levelsName.Add(level.Name);
                SaveLevel(level, $"{game.SavePath}/{level.Name}.json");
            }
            
            // Save world

            var worldStorage = new WorldStorage()
            {
                Time =  game.World.DayNightCycle.Time,
                PlayerSpawnLevel = game.World.PlayerSpawnLevel,
                Levels = levelsName
            };
            
            // Save players
            
            Directory.CreateDirectory(game.SavePath + "/remotes_players");
            
            File.WriteAllText($"{game.SavePath}/player.json", game.MainPlayer.Save().ToJson());
            
            File.WriteAllText($"{game.SavePath}/game.json", worldStorage.ToJson());
        }


        public void SaveLevel(Level level, string path)
        {
            var storage = new LevelStorage()
            {
                Id = level.Id,
                Name = level.Name,
                Type = level.Properties.Name,
                    
                Width = level.Width,
                Height = level.Height,
                    
                Tiles = level.Tiles,
                TilesData = level.TilesData
                
            };

            foreach (var e in level.Entities)
            {
                if (!ENTITIES.SaveExluded.Contains(e.Blueprint))
                {
                    storage.Entities.Add(e.Save());
                }
            }
            
            File.WriteAllText(path, storage.ToJson());
            Logger.Log<TaskSaveWorld>($"Level saved to {path}");
        }
    }
}
