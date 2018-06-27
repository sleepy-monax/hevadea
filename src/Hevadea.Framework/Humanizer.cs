using System;

namespace Hevadea.Framework
{
    public static class Humanizer
    {
        public static string Humanize(this TimeSpan timeSpan)
        {
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            if (seconds < 10)
                return minutes + ":0" + seconds;
            else
                return minutes + ":" + seconds;
        }
    }
}