using System.Collections.Generic;
using System.IO;
using Hevadea.Framework.Utils;
using Hevadea.Framework.Utils.Json;
using Hevadea.Game.Entities;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Hevadea.Game.Worlds;

namespace Hevadea.Game.Loading
{
    public class LoadingTaskLoadWorld : LoadingTask
    {
        public override string TaskName => "load_world";
        
        public override void Task(GameManager game)
        {
            var worldData = File.ReadAllText($"{game.SavePath}/game.json").FromJson<WorldStorage>();
            var world = new World();
            var player = (EntityPlayer)ENTITIES.PLAYER.Construct();
            player.Load(File.ReadAllText($"{game.SavePath}/player.json").FromJson<EntityStorage>());
            
            foreach (var levelName in worldData.Levels)
            {
                Logger.Log<LoadingTaskLoadWorld>(LoggerLevel.Info, $"Loading level '{levelName}'...");
                world.Levels.Add(LoadLevel($"{game.SavePath}/{levelName}.json"));
            }

            game.World = world;
            game.MainPlayer = player;
        }

        public Level LoadLevel(string path)
        {
            var levelData = File.ReadAllText(path).FromJson<LevelStorage>();
            var level = new Level(LEVELS.GetProperties(levelData.Type), levelData.Width, levelData.Height)
            {
                Name = levelData.Name,
                Id = levelData.Id,
                Tiles = levelData.Tiles,
                TilesData = levelData.TilesData,
            };

            foreach (var item in levelData.Entities)
            {
                var e = ENTITIES.GetBlueprint(item.Type).Construct();
                e.Load(item);
                level.AddEntity(e);
            }

            return level;
        }
    }
}
