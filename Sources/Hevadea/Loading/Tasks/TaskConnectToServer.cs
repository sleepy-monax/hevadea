using System;

namespace Hevadea.Loading.Tasks
{
    public class TaskConnectToServer : LoadingTask
    {
        private readonly string _ip;
        private readonly int _port;
        
        public TaskConnectToServer(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }
        
        public override void Task(GameManager.GameManager game)
        {
            SetStatus($"Connecting to: {_ip}:{_port}...");
            
            if (!game.Connect(_ip, _port))
            {
                SetStatus("Connection Failed!");
                throw new Exception("Connection failed!");
            }
            
            SetStatus("Connection succed!");
        }
    }
}
