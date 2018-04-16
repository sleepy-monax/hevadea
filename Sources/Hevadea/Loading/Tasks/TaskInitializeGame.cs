namespace Hevadea.Loading.Tasks
{
    public class TaskInitializeGame : LoadingTask
    {
        public override void Task(GameManager.GameManager game)
        {
            SetStatus("Initializing game...");
            game.Initialize();
        }
    }
}