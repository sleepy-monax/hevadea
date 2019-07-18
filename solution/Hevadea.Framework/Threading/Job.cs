using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hevadea.Framework.Threading
{
    public class Job
    {
        private float _progress = 0f;
        private string _status = "";
        private JobHandler _job;
        private IJobArguments _args;

        public delegate object JobHandler(Job task, IJobArguments args);

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

        public Job SetArguments(IJobArguments args)
        {
            _args = args;
            return this;
        }

        public Job Start(bool async = true)
        {
            Logger.Log<Job>("Starting the job...");
            try
            {
                if (!Started)
                {
                    Started = true;
                    if (async)
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
            if (Canceled) throw new TaskCanceledException();
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