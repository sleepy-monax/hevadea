using Hevadea.Framework.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hevadea.Framework.Threading
{
    public class JobArguments
	{
	}
   
	public class Job
    {
        float _progress = 0f;
        string _status = "";
        public delegate object JobHandler(Job task, JobArguments args);

        JobHandler _job;
		JobArguments _args;

        public bool Started { get; private set; }
        public bool Canceled { get; private set; }
        public bool Finished { get; private set; }

        public event EventHandler Finish;
        public event EventHandler ProgressChanged;
        public event EventHandler StatusChanged;
        public event EventHandler<Exception> Exception;

        public object Result { get; private set; }
        public string Name { get; }
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public float Progress
        {
            get => _progress;
            set
            {
                _progress = Mathf.Clamp01(value);
                ProgressChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Job(JobHandler job)
        {
            Name = "null";
            _job = job;
        }

        public Job(string name, JobHandler job)
        {
            Name = name;
            _job = job;
        }

		public static Job NewEmpty(string name)
        {
			return new Job(name, null);
        }

		public Job SetArguments(JobArguments args)
		{
			_args = args;
			return this;
		}

		public Job Start(bool paralel = true)
        {
            try
            {
                if (!Started)
                {
                    Started = true;
                    if (paralel)
                    {
                        Task.Run(() =>
                        {
							if (_job != null)
                                Result = _job.Invoke(this, _args);
							
                            Finish?.Invoke(this, EventArgs.Empty);
                            Finished = true;
                        });
                    }
                    else
                    {
						if (_job != null)
                            Result = _job.Invoke(this, _args);

						Finished = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log(LoggerLevel.Warning, "Finish with exception!");
                Logger.Log(ex);
                Exception?.Invoke(this, ex);
            }

            return this;
        }

        public void ThrowIfCanceled()
        {
            if (Canceled)
            {
                throw new TaskCanceledException();
            }
        }

        public void Cancel()
        {
            Canceled = true;
        }

        public void Wait()
        {
			while (!(Canceled || Finished))
                Thread.Sleep(10);
        }

        public void Report(string status)
        {
            Status = status;
            Progress = 0;
            Log(LoggerLevel.Info, status);
        }

        public void Report(float progress)
        {
            Progress = progress;
        }
        
        public void Log(LoggerLevel level, string msg)
        {
            Logger.Log("Job:" + Name, level, msg);
        }

        public Job Then(EventHandler action)
        {
            Finish += action;
            return this;
        }
    }
}