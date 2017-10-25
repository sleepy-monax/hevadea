using System;

namespace WorldOfImagination.Framework
{
    public enum LogType { Info, Warning, Error, Message }

    public static class Debugger
    {
        public static Action<string> OnLogOutput;

        public static void WriteLog(string text, LogType logMessageType, string senderName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[{DateTime.Now}] ");

            switch (logMessageType)
            {
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogType.Message:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            Console.Write($"[{logMessageType.ToString()}] {senderName}: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(text);
            OnLogOutput?.Invoke($"[{logMessageType.ToString()}] {senderName}: {text}");
        }
    }
}
