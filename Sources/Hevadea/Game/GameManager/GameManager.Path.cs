using Hevadea.Game.Worlds;

namespace Hevadea.Game
{
    public partial class GameManager
    {
        public string GetRemotePlayerPath()
            => $"{SavePath}/remotes_players";
        
        public string GetGameSaveFile()
            => $"{SavePath}/game.json";
        
        public string GetPlayerSaveFile()
            => $"{SavePath}/player.json";
        
        public string GetLevelSavePath(Level level)
            => $"{SavePath}/{level.Name}.json";
        
        public string GetLevelSavePath(string level)
            => $"{SavePath}/{level}.json";
    }
}