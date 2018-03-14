using Hevadea.Framework.Utils;
using System;
using System.Threading;

namespace Hevadea.Game.Loading
{
    public abstract class LoadingTask
    {

        private float _progress;
        public abstract string TaskName { get; }
        public Thread Thread { get; set; }
        public bool HasFinish { get; private set; } = false;
        public Exception Exception { get; private set; } = null;

        public void RunTask(GameManager game)
        {
            Thread = new Thread(TaskInternal);
            Thread.Start(game);
        }

        public void SetProgress(float progress)
        {
            _progress = Mathf.Clamp01(progress);
        }

        public virtual float GetProgress()
        {
            return _progress;
        }

        private void TaskInternal(object game)
        {
            try
            {
                Task((GameManager)game);
            }
            catch (Exception ex)
            {
                Exception = ex;
                Logger.Log<LoadingTask>(ex.ToString());
            }
            HasFinish = true;
        }
        public abstract void Task(GameManager game);
    }
}
