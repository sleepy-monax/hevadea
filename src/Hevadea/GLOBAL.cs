using Hevadea.Framework;

namespace Hevadea
{
    public static class GLOBAL
    {
        public static int Unit = 16;
        public static string ApplicationName = "Hevadea";

        public static string GetSavePath()
        {
            return Rise.Platform.GetStorageFolder() + "/Saves/";
        }
    }
}