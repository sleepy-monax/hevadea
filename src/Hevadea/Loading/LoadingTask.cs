using Hevadea.Framework.Utils;
using System;
using System.Threading;

namespace Hevadea.Loading
{
    public abstract class LoadingTask
    {
        private float _progress;
        private string _status;

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

        public void SetStatus(string status)
        {
            _status = status;
        }

        public virtual string GetStatus()
        {
            return _status;
        }

        private void TaskInternal(object game)
        {
#if !DEBUG
            try
            {
#endif
            Task((GameManager)game);
#if !DEBUG
            }
            catch (ThreadAbortException) {}
            catch (Exception ex)
            {
                Exception = ex;
                Logger.Log<LoadingTask>(ex.ToString());
            }
#endif
            HasFinish = true;
        }

        public abstract void Task(GameManager game);
    }
}