using Newtonsoft.Json;

namespace Hevadea.Storage
{
    [JsonObject]
    public class LevelStorage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }


    }
}