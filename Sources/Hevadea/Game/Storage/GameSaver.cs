using Hevadea.Framework.Utils;
using Hevadea.Framework.Utils.Json;
using System.IO;

namespace Hevadea.Game.Storage
{
    public class GameSaver
    {
        private string _status;
        private bool _isDone = false;
        private float _progress = 0f;

        private void SetStatus(string status)
        {
            _status = status;
            Logger.Log<GameManager>(LoggerLevel.Info, status);
        }
        
        public void Save(string path, GameManager game)
        {
            Logger.Log<GameManager>(LoggerLevel.Info, $"Saving world to '{path}'.");
            Directory.CreateDirectory(path);

            SetStatus("Saving player data...");
            var p = game.MainPlayer;
            var player = p.Save();
            p.Level.RemoveEntity(p);
            _progress = 0.25f;

            SetStatus("Saving world data...");
            var world = game.World.Save();
            p.Level.AddEntity(p);
            _progress = 0.50f;

            SetStatus("Encoding data...");
            string worldJson = world.ToJson();
            string playerJson = player.ToJson();
            _progress = 0.75f;

            SetStatus("Saving to files...");
            File.WriteAllText($"{path}/world.json", worldJson);
            File.WriteAllText($"{path}/player.json", playerJson);
            _progress = 1f;

            Logger.Log<GameManager>(LoggerLevel.Fine, "Done!");
            _isDone = true;
        }

        public float GetProgress()
        {
            return _progress;
        }

        public string GetStatus()
        {
            return _status;
        }
    }
}