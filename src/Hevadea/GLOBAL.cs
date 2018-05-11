using Hevadea.Framework;

namespace Hevadea
{
    public static class GLOBAL
    {
        public static int Unit = 16;
        public static string ApplicationName = "Hevadea";
        public static string ApplicationVersionName = "0.1.0";
        public static int ApplicationVersionId = 1;

        public static string GetSavePath()
        {
            return Rise.Platform.GetStorageFolder() + "/Saves/";
        }
    }
}