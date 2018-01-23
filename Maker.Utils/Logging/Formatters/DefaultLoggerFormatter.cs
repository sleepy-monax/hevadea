namespace Maker.Utils.Logging.Formatters
{
    internal class DefaultLoggerFormatter : ILoggerFormatter
    {
        public string ApplyFormat(LogMessage logMessage)
        {
            return string.Format("{0:HH:mm:ss} {1} [{3}::{4}() ln{2}] {5}",
                            logMessage.DateTime, logMessage.Level, logMessage.LineNumber, logMessage.CallingClass,
                            logMessage.CallingMethod, logMessage.Text);
        }
    }
}
