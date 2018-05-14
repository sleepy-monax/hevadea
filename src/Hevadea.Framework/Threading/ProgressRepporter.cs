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
      
        event EventHandler<float> ProgressChange;
		event EventHandler<string> StatusChange;

		public void RepportStatus(string status)
		{
			Status = status;
			StatusChange?.Invoke(this, status);
		}

        public void Report(float progress)
        {
            Progress = progress;         
            ProgressChange?.Invoke(this, Progress);
        }

    }
}
