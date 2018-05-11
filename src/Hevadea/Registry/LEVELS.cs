using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Registry
{
    public static class LEVELS
    {
        private static Dictionary<string, LevelProperties> _levelProperties = new Dictionary<string, LevelProperties>();

        public static LevelProperties SURFACE;
        public static LevelProperties UNDERGROUND;

        public static LevelProperties GetProperties(string name)
        {
            return _levelProperties.ContainsKey(name) ? _levelProperties[name] : SURFACE;
        }

        public static LevelProperties Register(LevelProperties level)
        {
            _levelProperties.Add(level.Name, level);
            return level;
        }

        public static void Initialize()
        {
            SURFACE = Register(new LevelProperties("surface", true, Color.White));
            UNDERGROUND = Register(new LevelProperties("underground", false, Color.Black));
        }
    }
}