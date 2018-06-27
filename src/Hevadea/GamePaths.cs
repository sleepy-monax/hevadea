using Hevadea.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea
{
    public static class GamePaths
    {

        public static string LastGameFile => Rise.Platform.GetStorageFolder().CombineWith("/LastGame");
        public static string ConfigFile => Rise.Platform.GetStorageFolder().CombineWith("/Config.json");
        public static string ServersFile => Rise.Platform.GetStorageFolder().CombineWith("/Servers.json");

        public static string SavesFolder   => Rise.Platform.GetStorageFolder().CombineWith("/Saves/");
        public static string ModsFolder    => Rise.Platform.GetStorageFolder().CombineWith("/Mods/");
        public static string PlayersFolder => Rise.Platform.GetStorageFolder().CombineWith("/Players/");

        public static void Initialize()
        {
            Directory.CreateDirectory(SavesFolder);
            Directory.CreateDirectory(ModsFolder);
            Directory.CreateDirectory(PlayersFolder);
        }

        public static string CombineWith(this string basePath, string relative)
        {
            if (String.IsNullOrWhiteSpace(basePath))
                throw new ArgumentNullException(nameof(basePath));

            if (String.IsNullOrWhiteSpace(relative))
                return basePath;

            basePath = basePath.TrimEnd('/');
            relative = relative.TrimStart('/');

            return $"{basePath}/{relative}";
        }
    }
}
