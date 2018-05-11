namespace Hevadea.Framework.Threading
{
    public class AsyncTask
    {
        public bool Done { get; set; } = false;

        public delegate void TaskHandler();

        public TaskHandler Task;

        public AsyncTask(TaskHandler task)
        {
            Task = task;
        }
    }
}