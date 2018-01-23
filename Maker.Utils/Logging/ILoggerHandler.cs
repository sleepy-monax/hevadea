namespace Maker.Utils.Logging
{
    public interface ILoggerHandler
    {
        void Publish(LogMessage logMessage);
    }
}
