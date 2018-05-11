using System;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Loading
{
    public class TaskCompound
    {
        LoadingTask _currentTask;
        Queue<LoadingTask> _tasksQueue;
        List<LoadingTask> _finishedTasks = new List<LoadingTask>();
        GameManager _game;

        bool _started = false;
        bool _aborted = false;

        public List<LoadingTask> Tasks { get; set; } = new List<LoadingTask>();
        public Action LoadingFinished;
        public Action<Exception> LoadingException;

        public TaskCompound()
        {
            _game = new GameManager();
        }

        public TaskCompound(string gamePath)
        {
            _game = new GameManager() { SavePath = gamePath };
        }

        public TaskCompound(GameManager game)
        {
            _game = game;
        }

        public void Start()
        {
            if (!_started && !_aborted)
            {
                _tasksQueue = new Queue<LoadingTask>(Tasks);
                _started = true;
            }
        }

        public void Abort()
        {
            if (_started && !_aborted)
            {
                _currentTask?.Thread.Abort();
                _tasksQueue.Clear();
                _started = false;
                _aborted = true;
            }
        }

        public List<LoadingTask> GetPendingTasks()
        {
            return _tasksQueue.ToList();
        }

        public List<LoadingTask> GetFinishedTasks()
        {
            return _finishedTasks;
        }

        public LoadingTask GetRunningTask()
        {
            return _currentTask;
        }

        public GameManager GetGame()
        {
            return _game;
        }

        public void Update()
        {
            if ((_currentTask == null || _currentTask.HasFinish) && _started)
            {
                if (_currentTask != null)
                {
                    _finishedTasks.Add(_currentTask);
                }

                if (_tasksQueue.Count > 0)
                {
                    if (_currentTask!= null && _currentTask.Exception != null)
                    {
                        Abort();
                        LoadingException?.Invoke(_currentTask.Exception);
                    }
                    else
                    {
                        _currentTask = _tasksQueue.Dequeue();
                        _currentTask.RunTask(_game);
                    }
                }
                else
                {
                    _currentTask = null;
                    LoadingFinished?.Invoke();
                }
            }
        }

    }
}
