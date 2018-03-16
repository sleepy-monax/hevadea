namespace Hevadea.Game.Loading.Tasks
{
    public class TaskInitializeGame : LoadingTask
    {
        public override void Task(GameManager game)
        {
            SetStatus("Initializing game...");
            game.Initialize();
        }
    }
}