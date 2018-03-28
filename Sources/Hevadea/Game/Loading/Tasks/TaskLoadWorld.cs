using Hevadea.Framework.Utils.Json;
using Hevadea.Game.Entities;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Hevadea.Game.Worlds;
using System.IO;
using Hevadea.Framework;
using Hevadea.Framework.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Loading.Tasks
{
    public class TaskLoadWorld : LoadingTask
    {        
        public override void Task(GameManager game)
        {
            SetStatus("Loading world...");
            
            var world = new World();
            var player = (EntityPlayer)ENTITIES.PLAYER.Construct();
            var worldData = File.ReadAllText(game.GetGameSaveFile()).FromJson<WorldStorage>();
            
            player.Load(File.ReadAllText(game.GetPlayerSaveFile()).FromJson<EntityStorage>());

            foreach (var levelName in worldData.Levels)
            {
                SetStatus($"Loading level the '{levelName}'...");
                
                var level = LoadLevel(levelName, game.GetLevelSavePath(levelName));

                if (File.Exists(game.GetLevelMinimapSavePath(level)))
                {                
                    var task = new AsyncTask(() =>
                    {
                        var fs = new FileStream(game.GetLevelMinimapSavePath(level), FileMode.Open);
                        level.Map = Texture2D.FromStream(Rise.MonoGame.GraphicsDevice, fs);
                        fs.Close();                    
                    });

                    Rise.AsyncTasks.Add(task);
                    
                    while (!task.Done) { }
                }

     
                world.Levels.Add(level);
                
            }

            game.World = world;
            game.MainPlayer = player;
        }

        public Level LoadLevel(string levelName, string path)
        {
            SetProgress(0);
            SetStatus($"Reading the '{levelName}' from disk...");
            var levelData = File.ReadAllText(path).FromJson<LevelStorage>();
            
            SetStatus($"Decoding data of the '{levelName}'...");
            var level = new Level(LEVELS.GetProperties(levelData.Type), levelData.Width, levelData.Height)
            {
                Name = levelData.Name,
                Id = levelData.Id,
                Tiles = levelData.Tiles,
                TilesData = levelData.TilesData,
            };

            SetStatus($"Loading entities of the '{level.Name}'...");
            float entityCount = levelData.Entities.Count;
            
            for (var i = 0; i < levelData.Entities.Count; i++)
            {
                var item = levelData.Entities[i];
                var e = ENTITIES.GetBlueprint(item.Type).Construct();
                e.Load(item);
                level.AddEntity(e);
                
                SetProgress(i / entityCount);
            }

            SetProgress(100);
            return level;
        }
    }
}
