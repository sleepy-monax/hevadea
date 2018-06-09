namespace Hevadea.Framework
{
    public class GCListener
    {
        public static void Start() => new GCListener();

        ~GCListener()
        {
            Logger.Log<GCListener>("Garbage Colected!");
            new GCListener();
        }
    }
}