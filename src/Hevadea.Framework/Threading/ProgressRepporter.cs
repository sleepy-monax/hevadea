using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Threading
{
    public class ProgressRepporter<T>
    {
        public T Progress { get; private set; }
      
        event EventHandler<T> ProgressChange;

        public void Report(T progress)
        {
            lock (Progress)
            {
                Progress = progress;
            }

            ProgressChange?.Invoke(this, Progress);
        }

    }
}
