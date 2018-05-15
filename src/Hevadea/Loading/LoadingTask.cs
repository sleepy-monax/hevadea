using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Hevadea.Framework.Threading;

namespace Hevadea.Loading
{
    public class LoadingTask
    {

        Thread _thread;

        public delegate void LoadingTaskJob(LoadingTask task, ProgressRepporter progressRepporter);

        public event EventHandler LoadingAborted;
        public event EventHandler LoadingFinished;
        public event EventHandler<Exception> LoadingException;

        public Object Result { get; set; }
		public ProgressRepporter ProgressRepporter { get; private set; }
        public bool Started { get; private set; } = false;
  
		public LoadingTask(LoadingTaskJob task)
        {
			ProgressRepporter = new ProgressRepporter();
			_thread = new Thread(() => 
			{
				task(this, ProgressRepporter);
				LoadingFinished?.Invoke(this, EventArgs.Empty);
			});
        }
        
        public void Start()
        {
            if (!Started)
            {
				_thread.Start();
                Started = true;
            }
        }

        public void Abort()
        {
            if (Started )
            {
                _thread.Abort();
                Started = false;
				LoadingAborted?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}