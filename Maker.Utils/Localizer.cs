using System.Collections.Generic;

namespace Maker.Utils
{
    public static class Localizer
    {
        private static Dictionary<string, string> _textes = new Dictionary<string, string>();

        public static void LoadLocalizationFile()
        {}
        
        public static string GetText(string id)
        {
            if (_textes.ContainsKey(id))
            {
                return _textes[id];
            }

            return id;
        }
    }
}