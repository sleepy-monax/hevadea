using System;

namespace Hevadea.Framework.Extension
{
    public static class HumanizeExtension
    {
        public static string Humanize(this TimeSpan timeSpan)
        {
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            return (minutes < 10 ? "0" : "") + minutes + (seconds < 10 ?  ":0" : ":") + seconds;
        }
    }
}