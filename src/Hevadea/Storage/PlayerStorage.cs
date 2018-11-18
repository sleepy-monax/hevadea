using Newtonsoft.Json;

namespace Hevadea.Storage
{
    [JsonObject]
    public class PlayerStorage
    {
        public string Name { get; set; }
        public int Token { get; set; }
        public EntityStorage Entity { get; set; }

        public PlayerStorage() { }
    }
}