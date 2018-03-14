using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Framework.Utils;
using Hevadea.Framework.Utils.Json;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Hevadea.Game.Worlds;

namespace Hevadea.Game.Loading
{
    public class LoadingTaskSaveWorld : LoadingTask
    {
        public override string TaskName => "save_world";
        
        public override void Task(GameManager game)
        {
            var levelsName = new List<string>();

            Directory.CreateDirectory(game.SavePath);
            
            // Saves levels
            
            foreach (var level in game.World.Levels)
            {
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
                    
                Tiles = level.Tiles
            };

            foreach (var e in level.Entities)
            {
                if (!ENTITIES.SaveExluded.Contains(e.Blueprint))
                {
                    storage.Entities.Add(e.Save());
                }
            }
            
            File.WriteAllText(path, storage.ToJson());
            Logger.Log<LoadingTaskSaveWorld>($"Level saved to {path}");
        }
    }
}
