using Hevadea.Framework;

namespace Hevadea
{
    public static class Constant
    {
        public static int TileSize = 16;
        public static string ApplicationName = "Hevadea";

        public static string GetSavePath()
        {
            return Rise.Platform.GetStorageFolder() + "/Saves/";
        }
    }
}