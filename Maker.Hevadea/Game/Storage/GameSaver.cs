using System.IO;
using Maker.Utils;
using Maker.Utils.Enums;
using Maker.Utils.Json;

namespace Maker.Hevadea.Game.Storage
{
    public class GameSaver
    {
        private string _status;
        private bool _isDone = false;

        private void SetStatus(string status)
        {
            _status = status;
            Logger.Log<GameManager>(LoggerLevel.Info, status);
        }
        
        public void Save(string path, GameManager game)
        {
            Logger.Log<GameManager>(LoggerLevel.Info, $"Saving world to '{path}'.");
            Directory.CreateDirectory(path);

            SetStatus("Saving player data");
            var p = game.Player;
            var player = p.Save();
            p.Level.RemoveEntity(p);
            SetStatus("Saving world");
            var world = game.World.Save();
            p.Level.AddEntity(p);

            SetStatus("Serialization");
            string worldJson = world.ToJson();
            string playerJson = player.ToJson();
            
            SetStatus("Saving to files");
            File.WriteAllText($"{path}\\world.json", worldJson);
            File.WriteAllText($"{path}\\player.json", playerJson);
            
            Logger.Log<GameManager>(LoggerLevel.Fine, "Done!");
            _isDone = true;
        }

        public string GetStatus()
        {
            return _status;
        }
    }
}