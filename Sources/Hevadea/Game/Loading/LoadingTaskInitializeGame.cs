namespace Hevadea.Game.Loading
{
    public class LoadingTaskInitializeGame : LoadingTask
    {
        public override string TaskName => "initialize_game";
        public override void Task(GameManager game)
        {
            game.Initialize();
        }
    }
}