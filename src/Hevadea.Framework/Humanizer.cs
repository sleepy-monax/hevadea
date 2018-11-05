using System;

namespace Hevadea.Framework
{
    public static class Humanizer
    {
        public static string Humanize(this TimeSpan timeSpan)
        {
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            return (minutes < 10 ? "0" : "") + minutes + (seconds < 10 ?  ":0" : ":") + seconds;
        }
    }
}