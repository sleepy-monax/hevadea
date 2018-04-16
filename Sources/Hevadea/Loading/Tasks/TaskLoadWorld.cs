using System.Collections.Generic;
using System.IO;
using Hevadea.Framework;
using Hevadea.Framework.Threading;
using Hevadea.Framework.Utils.Json;
using Hevadea.GameObjects.Entities.Blueprints.Legacy;
using Hevadea.Registry;
using Hevadea.Storage;
using Hevadea.Worlds;
using Hevadea.Worlds.Level;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Loading.Tasks
{
    public class TaskLoadWorld : LoadingTask
    {        
        public override void Task(GameManager.GameManager game)
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
                
                if (File.Exists(game.GetLevelMinimapDataPath(level)))
                {
                    level.Minimap.Waypoints = File.ReadAllText(game.GetLevelMinimapDataPath(level)).FromJson<List<MinimapWaypoint>>() 
                                              ?? new List<MinimapWaypoint>();
                }
                
                if (File.Exists(game.GetLevelMinimapSavePath(level)))
                {                
                    var task = new AsyncTask(() =>
                    {
                        var fs = new FileStream(game.GetLevelMinimapSavePath(level), FileMode.Open);
                        level.Minimap.Texture = Texture2D.FromStream(Rise.MonoGame.GraphicsDevice, fs);
                        fs.Close();                    
                    });

                    Rise.AsyncTasks.Add(task);
                    
                    while (!task.Done)
                    {
                        // XXX: Hack to fix the soft lock when loading the world.
                        System.Threading.Thread.Sleep(10);
                    }
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
                TilesData = levelData.TilesData,
            };

            for (int i = 0; i < levelData.Width * levelData.Height; i++)
            {
                level.Tiles[i] = TILES.GetTile(levelData.TileBidinding[levelData.Tiles[i].ToString()]);
            }

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
