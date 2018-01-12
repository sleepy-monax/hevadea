namespace Maker.Rise.Logging
{
    public interface ILoggerHandler
    {
        void Publish(LogMessage logMessage);
    }
}
