using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Threading
{
    public class ProgressRepporter
    {
        public float Progress { get; private set; }
		public string Status { get; private set; }

        public event EventHandler<float> ProgressChange;
        public event EventHandler<string> StatusChange;

		public void RepportStatus(string status)
		{
			Logger.Log<ProgressRepporter>(LoggerLevel.Info, status);
			Status = status;
			StatusChange?.Invoke(this, status);
		}

        public void Report(float progress)
        {
			Logger.Log<ProgressRepporter>(LoggerLevel.Info, $"{Status}: {(int)(progress * 100)}%");
            Progress = progress;
            ProgressChange?.Invoke(this, Progress);
        }

    }
}
