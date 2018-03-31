namespace Hevadea.Loading.Tasks
{
    public class TaskSetupServer : LoadingTask
    {
        public int Port { get; set; } = GameManager.PORT;

        public override void Task(GameManager game)
        {
            SetStatus("Starting server...");
            game.StartServer(Port);
        }
    }
}
