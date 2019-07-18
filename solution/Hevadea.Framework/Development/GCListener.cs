namespace Hevadea.Framework.Development
{
    public class GCListener
    {
        public static void Start()
        {
            new GCListener();
            Logger.Log<GCListener>("Started!");
        }

        ~GCListener()
        {
            Logger.Log<GCListener>("Garbage Colected!");
            new GCListener();
        }
    }
}