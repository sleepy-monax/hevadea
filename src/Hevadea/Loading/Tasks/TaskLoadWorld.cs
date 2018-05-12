using Hevadea.Framework;
using Hevadea.Framework.Threading;
using Hevadea.Framework.Utils.Json;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.Registry;
using Hevadea.Storage;
using Hevadea.Worlds;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Hevadea.Loading.Tasks
{
    public class TaskLoadWorld : LoadingTask
    {
        public override void Task(GameManager game)
        {
            //SetStatus("Loading world...");

            //var world = new World();
            //var player = (EntityPlayer)EntityFactory.PLAYER.Construct();
            //var worldData = File.ReadAllText(game.GetGameSaveFile()).FromJson<WorldStorage>();

            //player.Load(File.ReadAllText(game.GetPlayerSaveFile()).FromJson<EntityStorage>());

            //foreach (var levelName in worldData.Levels)
            //{
            //    SetStatus($"Loading level the '{levelName}'...");

            //    var level = LoadLevel(levelName, game.GetLevelSavePath(levelName));

            //    if (File.Exists(game.GetLevelMinimapDataPath(level)))
            //    {
            //        level.Minimap.Waypoints = File.ReadAllText(game.GetLevelMinimapDataPath(level)).FromJson<List<MinimapWaypoint>>()
            //                                  ?? new List<MinimapWaypoint>();
            //    }

            //    if (File.Exists(game.GetLevelMinimapSavePath(level)))
            //    {
            //        // Texture should be load from the render thread.
            //        var task = new AsyncTask(() =>
            //        {
            //            var fs = new FileStream(game.GetLevelMinimapSavePath(level), FileMode.Open);
            //            level.Minimap.Texture = Texture2D.FromStream(Rise.MonoGame.GraphicsDevice, fs);
            //            fs.Close();
            //        });

            //        Rise.AsyncTasks.Add(task);

            //        while (!task.Done)
            //        {
            //            // XXX: Hack to fix the soft lock when loading the world.
            //            System.Threading.Thread.Sleep(10);
            //        }
            //    }

            //    world.Levels.Add(level);
            //}

            //game.World = world;
            //game.MainPlayer = player;
        }

    }
}