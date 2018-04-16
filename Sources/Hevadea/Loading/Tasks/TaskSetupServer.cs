namespace Hevadea.Loading.Tasks
{
    public class TaskSetupServer : LoadingTask
    {
        public int Port { get; set; } = GameManager.GameManager.PORT;

        public override void Task(GameManager.GameManager game)
        {
            SetStatus("Starting server...");
            game.StartServer(Port);
        }
    }
}
