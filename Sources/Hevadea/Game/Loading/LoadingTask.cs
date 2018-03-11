using Hevadea.Framework.Utils;
using System;
using System.Threading;

namespace Hevadea.Game.Loading
{
    public abstract class LoadingTask
    {

        private float _progress;
        public abstract string TaskName { get; }
        public bool HasFinish { get; private set; } = false;
        public Exception Exception { get; private set; } = null;

        public void RunTask(GameManager game)
        {
            new Thread(TaskInternal).Start(game);
        }

        public void SetProgress(float progress)
        {
            _progress = Mathf.Clamp01(progress);
        }

        public float GetProgress()
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
            }
            HasFinish = true;
        }
        public abstract void Task(GameManager game);
    }
}
