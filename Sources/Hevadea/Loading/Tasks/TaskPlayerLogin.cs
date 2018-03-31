using Hevadea.Framework.Utils;
using Hevadea.Networking;

namespace Hevadea.Loading.Tasks
{
    public class TaskPlayerLogin : LoadingTask
    {
        private string _playerName;
        private int    _playerToken;
     
        public TaskPlayerLogin(string playerName, int playerToken = -1)
        {
            _playerName = playerName;
            _playerToken = playerToken;
        }
        
        public override void Task(GameManager game)
        {
            SetStatus("Login in...");
            var connected = false;
            
            game.Dispacher.RegisterHandler(PacketType.Token, (socket, data)
                =>
            {
                _playerToken = data.ReadInteger();
                connected = true;
            });
            
            if (_playerToken == -1)
            {
                game.Client.SendData(PacketFactorie.ConstructLogin(_playerName, "null"));
            }
            else
            {
                game.Client.SendData(PacketFactorie.ConstructLogin(_playerName, _playerToken, "null"));
            }
            
            while (!connected){}
            
            Logger.Log<TaskPlayerLogin>(LoggerLevel.Info ,$"Loged in with player token '{_playerToken}'.");
        }
    }
}
