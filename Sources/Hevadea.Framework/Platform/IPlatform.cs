namespace Hevadea.Framework.Platform
{
    public interface IPlatform
    {
        void Initialize();
        string GetPlatformName();
        int GetScreenWidth();
        int GetScreenHeight();
    }
}