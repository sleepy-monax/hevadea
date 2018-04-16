namespace Hevadea.Loading.Tasks
{
    public class TaskDownloadWorld : LoadingTask
    {
        public override void Task(GameManager.GameManager game)
        {
            SetStatus("Downloading world...");
        }
    }
}
