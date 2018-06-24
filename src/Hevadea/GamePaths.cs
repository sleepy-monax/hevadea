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
        public static string LastGameFile => Rise.Platform.GetStorageFolder().CombineWith("/lastgame");
        public static string SavesFolder => Rise.Platform.GetStorageFolder().CombineWith("/Saves/");
    }
}
