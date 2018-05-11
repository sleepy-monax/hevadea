using System;

namespace Hevadea.Framework.Utils
{
    public enum LoggerLevel { None, Debug, Fine, Info, Warning, Error, Severe }

    public static class Logger
    {
        public static void Log(Exception ex)
        {
            Log("Exception", LoggerLevel.Error, ex.ToString());
        }

        public static void Log(string message)
        {
            Log("Logger", LoggerLevel.Info, message);
        }

        public static void Log(LoggerLevel level, string message)
        {
            Log("Logger", level, message);
        }

        public static void Log<T>(string message)
        {
            Log<T>(LoggerLevel.Info, message);
        }

        public static void Log<T>(LoggerLevel level, string message)
        {
            Log(typeof(T).Name, level, message);
        }

        public static void Log(string callingClass, LoggerLevel level, string message)
        {
            Console.WriteLine($"{level.ToString()} [{callingClass}] {message}");
        }
    }
}